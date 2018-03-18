using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.Models;
using MyExams.Enums;
using MyExams.ViewModels;

namespace MyExams.Repositories
{
    public class StudentsRepository : GenericRepository<Student>
    {

        // Exams of Student
        public List<ExamsOfStudent> GetExamsOfStudent(int id)
        {
            var query = from e in base.db.Exams
                        join c in base.db.Courses on e.CourseId equals c.CourseId
                        join t in base.db.Teachers on c.TeacherId equals t.TeacherId
                        where e.StudentId == id
                        orderby e.Date descending
                        select new ExamsOfStudent
                        {
                            Date = e.Date,
                            Course = new CourseDetails
                            {
                                Course = c.Name,
                                Teacher = new TeacherDetails
                                {
                                    TeacherFirstName = t.FirstName,
                                    TeacherLastName = t.LastName,
                                    TeacherTitle = (Title)t.Title
                                },
                                Credits = c.Credits
                            },
                            GradeToConvert = e.Grade,
                        };

            var examsOfStudent = query.ToList();

            for (var i = 0; i < examsOfStudent.Count(); i++)
            {
                examsOfStudent[i].Grade = (examsOfStudent[i].GradeToConvert.HasValue) ?
                    Math.Round((double)examsOfStudent[i].GradeToConvert, 2).ToString("F2") : "absent";
            }

            return examsOfStudent;
        }



        //Search for Student
        public IEnumerable<Student> GetAll(string sortOrder, string searchString)
        {
            IEnumerable<Student> students;
            if (String.IsNullOrEmpty(searchString))
            {
                searchString = "";
            }
            else
            {
                searchString = searchString.ToLower().Trim();
            }
            students = base.GetAll().Where(s => (s.FirstName + " " + s.LastName).ToLower().Contains(searchString)
                            || (s.LastName + " " + s.FirstName).ToLower().Contains(searchString)
                            || ((Specialization)s.Specialization).ToString().ToLower().Contains(searchString)
                            || ((YearOfStudy)s.YearOfStudy).ToString().ToLower().Contains(searchString)
                            || s.CNP.ToString().Contains(searchString));

            switch (sortOrder)
            {
                case "spec_asc":
                    return students.OrderBy(s => ((Specialization)s.Specialization).ToString());

                case "year_asc":
                    return students.OrderBy(s => ((YearOfStudy)s.YearOfStudy).ToString());

                case "cnps_asc":
                    return students.OrderBy(s => s.CNP);

                case "name_desc":
                    return students.OrderByDescending(s => s.FirstName + " " + s.LastName);

                case "spec_desc":
                    return students.OrderByDescending(s => ((Specialization)s.Specialization).ToString());

                case "year_desc":
                    return students.OrderByDescending(s => ((YearOfStudy)s.YearOfStudy).ToString());

                case "cnps_desc":
                    return students.OrderByDescending(s => s.CNP);

                default:
                    return students.OrderBy(s => s.FirstName + " " + s.LastName);
            }
        }


        //public string GetOrder(string sortOrder, ref string lastOrder)
        //{
        //    if (string.IsNullOrEmpty(sortOrder))
        //    {
        //        sortOrder = lastOrder;
        //    }
        //    else
        //    {
        //        if (lastOrder.Substring(0, 4) == sortOrder)
        //        {
        //            if (lastOrder.Substring(5, 3) == "asc")
        //            {
        //                sortOrder += "_desc";
        //            }
        //            else
        //            {
        //                sortOrder += "_asc";
        //            }
        //        }
        //        else
        //        {
        //            sortOrder += "_asc";
        //        }
        //        lastOrder = sortOrder;
        //    }
        //    return sortOrder;
        //}



        //for Student Situation View
        public bool StudentExsits(StudentSituation studentSituation)
        {
            var query = base.db.Students
                        .FirstOrDefault(s => s.StudentId == studentSituation.StudentId
                        && s.Specialization == studentSituation.Specialization
                        && s.YearOfStudy == studentSituation.YearOfStudy);

            if (query == null)
            {
                return false;
            }
            return true;
        }



        public StudentDetails GetStudentFullName(int id)
        {
            var query = from s in base.db.Students
                        where s.StudentId == id
                        select new StudentDetails
                        {
                            StudentFirstName = s.FirstName,
                            StudentLastName = s.LastName
                        };

            return query.ToList().First();
        }



        public StudentProfile GetStudentProfile(int id)
        {
            var query = from s in base.db.Students
                        where s.StudentId == id
                        select new StudentProfile
                        {
                            Specialization = (Specialization)s.Specialization,
                            YearOfStudy = (YearOfStudy)s.YearOfStudy
                        };

            return query.ToList().First();
        }



        public List<Exam> GetExamsForStudent(int id)
        {
            var query = from e in base.db.Exams
                        where e.StudentId == id
                        select e;

            return query.ToList();
        }



        public int GetTotalNumberOfExams(int id)
        {
            return GetExamsForStudent(id).Count();
        }



        public double CalculateAvgerage(int id)
        {
            var examsForStudent = GetExamsForStudent(id);

            if (examsForStudent.Count() == 0)
            {
                return 0;
            }

            var average = examsForStudent.Average(e => e.Grade);

            return (average.HasValue) ? Math.Round(average.Value, 2) : 0;
        }



        public int GetCreditsAcummulated(int id)
        {
            var query = from e in base.db.Exams
                        join c in base.db.Courses on e.CourseId equals c.CourseId
                        where ((e.StudentId == id) && (e.Grade != null && e.Grade >= 4.5))
                        select c.Credits;

            return query.ToList().Sum();
        }



        public List<string> GetExamsToRetake(int id)
        {
            var query = from e in base.db.Exams
                        join c in base.db.Courses on e.CourseId equals c.CourseId
                        where ((e.StudentId == id) && (e.Grade == null || e.Grade < 4.5))
                        select c.Name;

            return query.ToList();
        }




        //for Promotion Situation2 View
        public bool PromotionExists(PromotionSituation promotionSituation)
        {
            var query = base.db.Students
                        .FirstOrDefault(s => s.Specialization == promotionSituation.Specialization
                        && s.YearOfStudy == promotionSituation.YearOfStudy);

            return (query == null) ? false : true;
        }



        public List<int> GetPromotionIDs(PromotionSituation promotionSituation)
        {
            var query = from s in base.db.Students
                        where ((s.Specialization == promotionSituation.Specialization)
                        && (s.YearOfStudy == promotionSituation.YearOfStudy))
                        select s.StudentId;

            return query.ToList();
        }



        public double GetPromotionAverage(List<PromotionSituationTableAux> promotionSituation)
        {
            try
            {
                return Math.Round(promotionSituation.Where(a => (a.Average != 0 && a.ExamsCount != 0)).Average(a => a.Average), 2);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }



        public double GetHighestAverage(List<PromotionSituationTableAux> promotionSituation)
        {
            try
            {
                return Math.Round(promotionSituation.Max(a => a.Average), 2);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }



        public List<StudentDetails> GetScholarships(List<PromotionSituationTableAux> promotionSituation)
        {
            List<StudentDetails> studentScholarships = new List<StudentDetails>();

            var linqQuery = promotionSituation.Where(s => ((s.Average != 0) && (s.ExamsToRetakeCount == 0)))
                            .OrderByDescending(s => s.Average).Select(s => s.StudentId).ToList();

            switch (linqQuery.Count())
            {
                case 0:
                    return studentScholarships;

                case 1:
                    studentScholarships.Add(GetStudentFullName(linqQuery[0]));
                    return studentScholarships;

                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        studentScholarships.Add(GetStudentFullName(linqQuery[i]));
                    }
                    return studentScholarships;

                default:
                    for (int i = 0; i < 3; i++)
                    {
                        studentScholarships.Add(GetStudentFullName(linqQuery[i]));
                    }
                    return studentScholarships;

            }
        }

    }
}