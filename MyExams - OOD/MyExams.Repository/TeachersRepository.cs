using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Data;
using MyExams.Dto;
using MyExams.Contracts;
using AutoMapper;
using MyExams.Infrastructure;

namespace MyExams.Repository
{
    public class TeachersRepository : GenericRepository<Teachers, TeacherDto, TeacherDto>, ITeachersRepository
    {
        
        public List<TeacherDto> GetAll()
        {
            using (var db = new MyExamContext())
            {
                var query = from t in db.Teachers
                            select t;

                return Mapper.Map<List<Teachers>, List<TeacherDto>>(query.ToList());
            }
        }

        // get entities by search string in sort order
        public List<TeacherDto> GetSearch(string sortOrder, string searchString)
        {
            string searchStringEdit = base.GetSearchString(searchString);

            var teachers = GetAll().Where(t => (t.FirstName + t.LastName).ToLower().Contains(searchStringEdit)
                           || (t.LastName + t.FirstName).ToLower().Contains(searchStringEdit)
                           || ((Title)t.Title).ToString().ToLower().Contains(searchStringEdit));

            switch (sortOrder)
            {
                case "tnam_desc":
                    return teachers.OrderByDescending(t => t.FirstName + " " + t.LastName).ToList();

                case "titl_asc":
                    return teachers.OrderBy(t => ((Title)t.Title).ToString()).ToList();

                case "titl_desc":
                    return teachers.OrderByDescending(t => ((Title)t.Title).ToString()).ToList();

                default:
                    return teachers.OrderBy(t=> t.FirstName + " " + t.LastName).ToList();
            }
        }

        public SelectList GetSelectList(int? id = null)
        {
            using (var db = new MyExamContext())
            {
                var query = from t in db.Teachers
                            select new SelectListItem
                            {
                                Text = t.FirstName + " " + t.LastName,
                                Value = t.TeacherId.ToString()
                            };

                return new SelectList(query.ToList(), "Value", "Text", id);
            }
        }
    }
}
