using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.Models;
using MyExams.Enums;

namespace MyExams.Repositories
{
    public class ExamsRepository : GenericRepository<Exam>
    {
        public CoursesOfTeacher GetBasicCourseDetails(int id)
        {
            var query = from e in base.db.Exams
                        join c in base.db.Courses on e.CourseId equals c.CourseId
                        where e.ExamId == id
                        select new CoursesOfTeacher
                        {
                            Course = c.Name,
                            Credits = c.Credits
                        };

            return query.ToList().First();
        }

        public StudentDetails GetStudentDetails(int id)
        {
            var query = from e in base.db.Exams
                        join s in base.db.Students on e.StudentId equals s.StudentId
                        where e.ExamId == id
                        select new StudentDetails
                        {
                            StudentFirstName = s.FirstName,
                            StudentLastName = s.LastName
                        };

            return query.ToList().First();
        }

        public CourseDetails GetCourseDetails(int id)
        {
            var query = from e in base.db.Exams
                        join c in base.db.Courses on e.CourseId equals c.CourseId
                        join t in base.db.Teachers on c.TeacherId equals t.TeacherId
                        where e.ExamId == id
                        select new CourseDetails
                        {
                            Course = c.Name,
                            Teacher = new TeacherDetails
                            {
                                TeacherTitle = (Title)t.Title,
                                TeacherFirstName = t.FirstName,
                                TeacherLastName = t.LastName
                            },
                            Duration = c.Duration,
                            Credits = c.Credits
                        };

            return query.ToList().First();
        }

        public StudentProfile GetStudentProfile(int id)
        {
            var query = from e in base.db.Exams
                        join s in base.db.Students on e.StudentId equals s.StudentId
                        where e.ExamId == id
                        select new StudentProfile
                        {
                            Specialization = (Specialization)s.Specialization,
                            YearOfStudy = (YearOfStudy)s.YearOfStudy
                        };

            return query.ToList().First();

        }


        //Search for Exam
        public IEnumerable<Exam> GetAll(string sortOrder, string searchString)
        {
            IEnumerable<Exam> exams;
            if (String.IsNullOrEmpty(searchString))
            {
                searchString = "";
            }
            else
            {
                searchString = searchString.ToLower().Trim();
            }
            exams = base.GetAll().Where(e => (e.Date.ToString().Contains(searchString)
                           || (e.Student.FirstName + " " + e.Student.LastName).ToLower().Contains(searchString)
                           || e.Cours.Name.ToLower().Contains(searchString)
                           || e.Grade.ToString().Contains(searchString)
                           || e.Cours.Credits.ToString().Contains(searchString)));

            switch (sortOrder)
            {
                case "cour_asc":
                    return exams.OrderBy(e => (e.Cours.Name));

                case "snam_asc":
                    return exams.OrderBy(e => e.Student.FirstName + " " + e.Student.LastName);

                case "grad_asc":
                    return exams.OrderBy(e => e.Grade);

                case "cred_asc":
                    return exams.OrderBy(e => e.Cours.Name);
                case "date_desc":
                    return exams.OrderByDescending(e => (e.Date));

                case "cour_desc":
                    return exams.OrderByDescending(e => (e.Cours.Name));

                case "snam_desc":
                    return exams.OrderByDescending(e => e.Student.FirstName + " " + e.Student.LastName);

                case "grad_desc":
                    return exams.OrderByDescending(e => e.Grade);

                case "cred_desc":
                    return exams.OrderByDescending(e => e.Cours.Name);

                default:
                    return exams.OrderBy(e => (e.Date));

            }
        }
    }
}