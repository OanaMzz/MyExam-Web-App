using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.Models;
using MyExams.Enums;

namespace MyExams.Repositories
{
    public class CoursesRepository : GenericRepository<Cours>
    {

        public List<ExamsForCourse> GetExamsForCourse(int id)
        {
            var query = from e in base.db.Exams
                        join s in base.db.Students on e.StudentId equals s.StudentId
                        where e.CourseId == id 
                        orderby e.Date descending
                        select new ExamsForCourse
                        {
                            Date = e.Date,
                            Student = new StudentDetails
                            {
                                StudentFirstName = s.FirstName,
                                StudentLastName = s.LastName
                            },
                            StudentProfile = new StudentProfile
                            {
                                Specialization = (Specialization)s.Specialization,
                                YearOfStudy = (YearOfStudy)s.YearOfStudy
                            },
                            GradeToConvert = e.Grade
                        };

            var examsForCourse = query.ToList();

            for (var i = 0; i < examsForCourse.Count(); i++)
            {
                examsForCourse[i].Grade = (examsForCourse[i].GradeToConvert.HasValue) ?
                    Math.Round((double)examsForCourse[i].GradeToConvert, 2).ToString("F2") : "absent";
            }

            return examsForCourse;
        }



        public TeacherDetails GetTeacherDetails(int id)
        {
            var query = from c in base.db.Courses
                        join t in base.db.Teachers on c.TeacherId equals t.TeacherId
                        where c.CourseId == id
                        select new TeacherDetails
                        {
                            TeacherFirstName = t.FirstName,
                            TeacherLastName = t.LastName,
                            TeacherTitle = (Title)t.Title
                        };

            return query.ToList().First();
        }



        // Search for Course
        public IEnumerable<Cours> GetAll(string sortOrder, string searchString)
        {
            IEnumerable<Cours> courses;
            if (String.IsNullOrEmpty(searchString))
            {
                searchString = "";
            }
            else
            {
                searchString = searchString.ToLower().Trim();
            }
            courses = base.GetAll().Where(c => (c.Name).ToLower().Trim().Contains(searchString)
                            || (c.Duration).ToString().Contains(searchString)
                            || (c.Credits).ToString().Contains(searchString)
                            || (c.Teacher.FirstName + " " + c.Teacher.LastName).ToLower().Trim().Contains(searchString)
                            || (c.Teacher.LastName + " " + c.Teacher.FirstName).ToLower().Trim().Contains(searchString));

            switch (sortOrder)
            {
                case "tnam_asc":
                    return courses.OrderBy(c => (c.Teacher.FirstName + " " + c.Teacher.LastName));
                case "dura_asc":
                    return courses.OrderBy(c => (c.Duration));
                case "cred_asc":
                    return courses.OrderBy(c => c.Credits);

                case "tnam_desc":
                    return courses.OrderByDescending(c => (c.Teacher.FirstName + " " + c.Teacher.LastName));

                case "dour_desc":
                    return courses.OrderByDescending(c => (c.Duration));
                case "cred_desc":
                    return courses.OrderByDescending(c => c.Credits);
                case "name_desc":
                    return courses.OrderByDescending(c => c.Name);

                default:
                    return courses.OrderBy(c => c.Name);
            }
        }
    }
}