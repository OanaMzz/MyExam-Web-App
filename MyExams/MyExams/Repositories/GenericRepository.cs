using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using System.Data.Entity;

namespace MyExams.Repositories
{
    public class GenericRepository<T> where T : class
    {

        protected MyExamsEntities db = new MyExamsEntities();

        public  IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }

        public void Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw ex;
            }
        }


        // Find sort order in relationship with last sort order
        public string GetOrder(string sortOrder, ref string lastOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = lastOrder;
            }
            else
            {
                if (lastOrder.Substring(0, 4) == sortOrder)
                {
                    if (lastOrder.Substring(5, 3) == "asc")
                    {
                        sortOrder += "_desc";
                    }
                    else
                    {
                        sortOrder += "_asc";
                    }
                }
                else
                {
                    sortOrder += "_asc";
                }
                lastOrder = sortOrder;
            }
            return sortOrder;
        }
    }
}