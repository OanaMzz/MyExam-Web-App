using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Data;
using MyExams.Dto;
using MyExams.Contracts;
using AutoMapper;
using MyExams.Infrastructure;
using System.Web.Mvc;

namespace MyExams.Repository
{
    public class StudentsRepository : GenericRepository<Students, StudentsDto, StudentsDto>, IStudentsRepository
    {

        public List<StudentsDto> GetAll()
        {
            using (var db = new MyExamContext())
            {
                var query = from s in db.Students
                            select s;

                return Mapper.Map<List<Students>, List<StudentsDto>>(query.ToList());
            }
        }

        // get entities by search string in sort order
        public List<StudentsDto> GetSearch(string sortOrder, string searchString)
        {
            var searchStringEdit = base.GetSearchString(searchString);

            var students = GetAll().Where(s => (s.FirstName + s.LastName).ToLower().Contains(searchStringEdit)
                           || (s.LastName + s.FirstName).ToLower().Contains(searchStringEdit)
                           || ((Specialization)s.Specialization).ToString().ToLower().Contains(searchStringEdit)
                           || ((YearOfStudy)s.YearOfStudy).ToString().ToLower().Contains(searchStringEdit)
                           || s.CNP.ToString().Contains(searchStringEdit));

            switch (sortOrder)
            {
                case "name_desc":
                    return students.OrderByDescending(s => s.FirstName + " " + s.LastName).ToList();

                case "cnps_asc":
                    return students.OrderBy(s => s.CNP).ToList();

                case "cnps_desc":
                    return students.OrderByDescending(s => s.CNP).ToList();

                case "spec_asc":
                    return students.OrderBy(s => ((Specialization)s.Specialization).ToString()).ToList();
               
                case "spec_desc":
                    return students.OrderByDescending(s => ((Specialization)s.Specialization).ToString()).ToList();

                case "year_asc":
                    return students.OrderBy(s => ((YearOfStudy)s.YearOfStudy).ToString()).ToList();

                case "year_desc":
                    return students.OrderByDescending(s => ((YearOfStudy)s.YearOfStudy).ToString()).ToList();
             
                default:
                    return students.OrderBy(s => s.FirstName + " " + s.LastName).ToList();
            }
        }

        public SelectList GetSelectList(int? id = null)
        {
            using (var db = new MyExamContext())
            {
                var query = from s in db.Students
                            select new SelectListItem
                            {
                                Text = s.FirstName + " " + s.LastName,
                                Value = s.StudentId.ToString()
                            };

                return new SelectList(query.ToList(), "Value", "Text", id);
            }
        }

        public SelectList GetCurrentSpecializations(int? id = null)
        {
            using (var db = new MyExamContext())
            {
                var query = (from s in db.Students
                             select new SelectListItem
                             {
                                 Text = ((Specialization)s.Specialization).ToString(),
                                 Value = s.Specialization.ToString()

                             }).Distinct();

                return new SelectList(query.ToList(), "Value", "Text", id);
            }
        }

        public SelectList GetYearForSpecialization(int specialization, int? id = null)
        {
            using (var db = new MyExamContext())
            {
                var query = (from s in db.Students
                             where s.Specialization == specialization
                             select new SelectListItem
                             {
                                 Text = ((YearOfStudy)s.YearOfStudy).ToString(),
                                 Value = s.YearOfStudy.ToString()

                             }).Distinct();

                return new SelectList(query.ToList(), "Value", "Text", id);
            }
        }

    }
}
