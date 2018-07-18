using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Data;
using AutoMapper;
using System.Data.Entity;

namespace MyExams.Repository
{
    public class GenericRepository<T,C,U>
        where T : class  // typeOf(T) = MyExams.Data
        where C : class  // typeOf(C) = MyExams.Dto
        where U : class  // typeOf(U) = MyExams.Dto - special case
    {
        public C GetById(int id)
        {
            using (var db = new MyExamContext())
            {
                var query = db.Set<T>().Find(id);

                return Mapper.Map<T, C>(query);
            }
        }

        public void Add(C dto)
        {
            using (var db = new MyExamContext())
            {
                var entity = Mapper.Map<C, T>(dto);

                db.Set<T>().Add(entity);
                db.SaveChanges();
            }
        }

        public void Edit(C dto)
        {
            using (var db = new MyExamContext())
            {
                var entity = Mapper.Map<C, T>(dto);

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(C dto)
        {
            using (var db = new MyExamContext())
            {
                var entity = Mapper.Map<C, T>(dto);

                db.Set<T>().Attach(entity);
                db.Set<T>().Remove(entity);

                db.SaveChanges();
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

        // get search string
        public string GetSearchString(string searchString)
        {
            return (String.IsNullOrEmpty(searchString)) ? "" : searchString.Trim().ToLower();
        }

        // get current page number
        public void GetPagedListParameters(int? page, List<U> list, out int pageSize, out int pageNumber)
        {
            pageSize = 10;
            pageNumber = (page ?? 1);

            int noOfPage = (list.Count() / pageSize) + ((list.Count() % pageSize) == 0 ? 0 : 1);
            if (pageNumber > noOfPage)
            {
                pageNumber = 1;
            }
        }
    }
}
