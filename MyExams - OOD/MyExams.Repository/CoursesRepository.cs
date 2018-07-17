using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Data;
using MyExams.Dto;
using MyExams.Contracts;
using MyExams.Infrastructure;
using System.Web.Mvc;
using AutoMapper;

namespace MyExams.Repository
{
    public class CoursesRepository : GenericRepository<Courses, CourseDto, CourseDetailsDto>, ICoursesRepository
    {
        public List<CourseDetailsDto> GetAll()
        {
            using (var db = new MyExamContext())
            {
                var query = from c in db.Courses
                            join t in db.Teachers on c.TeacherId equals t.TeacherId
                            select new CourseDetailsDto
                            {
                                CourseId = c.CourseId,
                                Name = c.Name,
                                TeacherDetails = new TeacherDto
                                {
                                    TeacherId = t.TeacherId,
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Title = t.Title
                                },
                                Duration = c.Duration,
                                Credits = c.Credits
                            };

                return query.ToList();
            }
        }

        public CourseDetailsDto GetDetailsById(int id) // reuse for course details PW
        {
            using (var db = new MyExamContext())
            {
                var query = from c in db.Courses
                            join t in db.Teachers on c.TeacherId equals t.TeacherId
                            where c.CourseId == id
                            select new CourseDetailsDto
                            {
                                CourseId = c.CourseId,
                                Name = c.Name,
                                TeacherDetails = new TeacherDto
                                {
                                    TeacherId = t.TeacherId,
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Title = t.Title
                                },
                                Duration = c.Duration,
                                Credits = c.Credits
                            };

                return query.FirstOrDefault();
            }
        }

        public List<CourseDetailsDto> GetSearch(string sortOrder, string searchString)
        {
            var searchStringEdit = base.GetSearchString(searchString);

            var courses = GetAll().Where(c => (c.Name).Trim().ToLower().Contains(searchStringEdit)
                          || (c.TeacherDetails.FirstName + c.TeacherDetails.LastName).ToLower().Contains(searchStringEdit)
                          || (c.TeacherDetails.LastName + c.TeacherDetails.FirstName).ToLower().Contains(searchStringEdit)
                          || ((Title)c.TeacherDetails.Title).ToString().ToLower().Contains(searchStringEdit)
                          || (c.Duration).ToString().Contains(searchStringEdit)
                          || (c.Credits).ToString().Contains(searchStringEdit));

            switch (sortOrder)
            {
                case "cnam_desc":
                    return courses.OrderByDescending(c => c.Name).ToList();

                case "tnam_asc":
                    return courses.OrderBy(c => c.TeacherDetails.FirstName + " " + c.TeacherDetails.LastName).ToList();

                case "tnam_desc":
                    return courses.OrderByDescending(c => c.TeacherDetails.FirstName + " " + c.TeacherDetails.LastName).ToList();

                case "titl_asc":
                    return courses.OrderBy(c => ((Title)c.TeacherDetails.Title).ToString()).ToList();

                case "titl_desc":
                    return courses.OrderByDescending(c => ((Title)c.TeacherDetails.Title).ToString()).ToList();

                case "dura_asc":
                    return courses.OrderBy(c => c.Duration.ToString()).ToList();

                case "dura_desc":
                    return courses.OrderByDescending(c => c.Duration.ToString()).ToList();

                case "cred_asc":
                    return courses.OrderBy(c => c.Credits.ToString()).ToList();

                case "cred_desc":
                    return courses.OrderByDescending(c => c.Credits.ToString()).ToList();

                default:
                    return courses.OrderBy(c => c.Name).ToList();
            }
        }

        public SelectList GetSelectList(int? id = null)
        {
            using (var db = new MyExamContext())
            {
                var query = from c in db.Courses
                            select new SelectListItem
                            {
                                Text = c.Name,
                                Value = c.CourseId.ToString()
                            };

                return new SelectList(query.ToList(), "Value", "Text", id);
            }
        }

        public List<CourseDto> GetRelatedByPerson(int id)
        {
            using (var db = new MyExamContext())
            {
                var query = from c in db.Courses
                            where c.TeacherId == id
                            orderby c.Name ascending
                            select c;

                return Mapper.Map<List<Courses>, List<CourseDto>>(query.ToList());
            }
        }

    }
}
