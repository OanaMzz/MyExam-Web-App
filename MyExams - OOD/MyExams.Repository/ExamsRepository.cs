using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Data;
using MyExams.Dto;
using MyExams.Contracts;
using MyExams.Infrastructure;
using AutoMapper;

namespace MyExams.Repository
{
    public class ExamsRepository : GenericRepository<Exams, ExamDto, ExamDetailsDto>, IExamsRepository
    {

        public List<ExamDetailsDto> GetAll()
        {
            using (var db = new MyExamContext())
            {
                var query = from e in db.Exams
                            join c in db.Courses on e.CourseId equals c.CourseId
                            join s in db.Students on e.StudentId equals s.StudentId
                            select new ExamDetailsDto
                            {
                                ExamId = e.ExamId,
                                CourseId = c.CourseId,
                                Name = c.Name,
                                StudentDetails = new StudentsDto
                                {
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                },
                                Date = e.Date,
                                Grade = e.Grade,
                                Credits = c.Credits
                            };

                return query.ToList();
            }
        }

        public ExamDetailsDto GetDetailsById(int id)
        {
            using (var db = new MyExamContext())
            {
                var query = from e in db.Exams
                            join c in db.Courses on e.CourseId equals c.CourseId
                            join s in db.Students on e.StudentId equals s.StudentId
                            where e.ExamId == id
                            select new ExamDetailsDto
                            {
                                ExamId = e.ExamId,
                                CourseId = c.CourseId,
                                Name = c.Name,
                                StudentDetails = new StudentsDto
                                {
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                    Specialization = s.Specialization,
                                    YearOfStudy = s.YearOfStudy
                                },
                                Date = e.Date,
                                Grade = e.Grade,
                                Credits = c.Credits
                            };

                return query.FirstOrDefault();
            }
        }

        public List<ExamDetailsDto> GetSearch(string sortOrder, string searchString)
        {
            var searchStringEdit = base.GetSearchString(searchString);

            var exams = GetAll().Where(e => (e.Date).ToString().Contains(searchStringEdit)
                        || (e.Name).Trim().ToLower().Contains(searchStringEdit)
                        || (e.StudentDetails.FirstName + e.StudentDetails.LastName).ToLower().Contains(searchStringEdit)
                        || (e.StudentDetails.LastName + e.StudentDetails.FirstName).ToLower().Contains(searchStringEdit)
                        || (e.Grade).ToString().Contains(searchStringEdit)
                        || (e.Credits).ToString().Contains(searchStringEdit));

            switch (sortOrder)
            {
                case "date_desc":
                    return exams.OrderByDescending(e => e.Date).ToList();

                case "cnam_asc":
                    return exams.OrderBy(e => e.Name).ToList();

                case "cnam_desc":
                    return exams.OrderByDescending(e => e.Name).ToList();

                case "snam_asc":
                    return exams.OrderBy(e => e.StudentDetails.FirstName + " " + e.StudentDetails.LastName).ToList();

                case "snam_desc":
                    return exams.OrderByDescending(e => e.StudentDetails.FirstName + " " + e.StudentDetails.LastName).ToList();

                case "grad_asc":
                    return exams.OrderBy(e => e.Grade).ToList();

                case "grad_desc":
                    return exams.OrderByDescending(e => e.Grade).ToList();

                case "cred_asc":
                    return exams.OrderBy(e => e.Credits).ToList();

                case "cred_desc":
                    return exams.OrderByDescending(e => e.Credits).ToList();

                default:
                    return exams.OrderBy(e => e.Date).ToList();
            }
        }

        public List<ExamsOfStudentDto> GetRelatedByPerson(int id)
        {
            using (var db = new MyExamContext())
            {
                var query = from e in db.Exams
                            join c in db.Courses on e.CourseId equals c.CourseId
                            join t in db.Teachers on c.TeacherId equals t.TeacherId
                            where e.StudentId == id
                            orderby e.Date ascending
                            select new ExamsOfStudentDto
                            {
                                Date = e.Date,
                                Name = c.Name,
                                Teacher = new TeacherDto
                                {
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Title = t.Title
                                },
                                Grade = e.Grade,
                                Credits = c.Credits
                            };

                return query.ToList();
            }
        }

        public Dictionary<StudentsDto, List<ExamsOfStudentDto>> GetRelatedByPromotion(int specialization, int year)
        {
            var students = GetStudents(specialization, year);

            var result = new Dictionary<StudentsDto, List<ExamsOfStudentDto>>();

            foreach (var item in students)
            {
                result.Add(item, GetRelatedByPerson(item.StudentId));
            }

            return result;
        }

        private List<StudentsDto> GetStudents(int specialization, int year)
        {
            using (var db = new MyExamContext())
            {
                var query = from s in db.Students
                            where ((s.Specialization == specialization) && (s.YearOfStudy == year))
                            select s;

                return Mapper.Map<List<Students>, List<StudentsDto>>(query.ToList());
            }
        }

        public List<ExamDetailsDto> GetRelatedByCourse(int id)
        {
            using (var db = new MyExamContext())
            {
                var query = from e in db.Exams
                            join c in db.Courses on e.CourseId equals c.CourseId
                            join s in db.Students on e.StudentId equals s.StudentId
                            where c.CourseId == id
                            orderby e.Date ascending
                            select new ExamDetailsDto
                            {
                                Date = e.Date,
                                StudentDetails = new StudentsDto
                                {
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                    Specialization = s.Specialization,
                                    YearOfStudy = s.YearOfStudy                          
                                },
                                Grade = e.Grade,
                                Credits = c.Credits
                            };

                return query.ToList();
            }
        }
    }
}
