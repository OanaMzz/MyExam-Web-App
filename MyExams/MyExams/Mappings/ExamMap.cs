using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.ViewModels;
using MyExams.Repositories;

namespace MyExams.Mappings
{
    public class ExamMap
    {
        public static IEnumerable<ExamVM> ExamsToExamsVM(IEnumerable<Exam> exams)
        {
            var examsVM = new List<ExamVM>();

            foreach (var exam in exams)
            {
                var examVM = ExamToExamVM(exam);

                examsVM.Add(examVM);
            }

            return examsVM;
        }

        public static ExamVM ExamToExamVM(Exam exam)
        {
            var examsRepo = new ExamsRepository();

            var examVM = new ExamVM
            {
                ExamId = exam.ExamId,
                CourseId = exam.CourseId,
                StudentId = exam.StudentId,
                Date = exam.Date,
                Grade = (exam.Grade.HasValue) ? Math.Round((double)exam.Grade, 2).ToString("F2") : "absent",
                Course = examsRepo.GetBasicCourseDetails(exam.ExamId),
                Student = examsRepo.GetStudentDetails(exam.ExamId)
            };

            return examVM;
        }

        public static Exam ExamVMToExam(ExamVM examVM)
        {
            var exam = new Exam
            {
                ExamId = examVM.ExamId,
                CourseId = examVM.CourseId,
                StudentId = examVM.StudentId,
                Date = (DateTime)examVM.Date
            };

            if (examVM.Grade != null)
            {
                exam.Grade = double.Parse(examVM.Grade);
            }

            return exam;
        }
    }
}