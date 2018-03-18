using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.ViewModels;
using MyExams.Enums;
using System.Globalization;

namespace MyExams.Mappings
{
    public class StudentMap
    {
        public static IEnumerable<StudentVM> StudentsToStudentsVM(IEnumerable<Student> students)
        {
            var studentsVM = new List<StudentVM>();

            foreach (var student in students)
            {
                var studentVM = StudentToStudentVM(student);

                studentsVM.Add(studentVM);
            }
            return studentsVM;
        }

        public static StudentVM StudentToStudentVM(Student student)
        {
            var studentVM = new StudentVM()
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                CNP = student.CNP.ToString(),
                Specialization = (Specialization)student.Specialization,
                YearOfStudy = (YearOfStudy)student.YearOfStudy
            };
            return studentVM;
        }

        public static Student StudentVMToStudent(StudentVM studentVM)
        {
            var student = new Student
            {
                StudentId = studentVM.StudentId,
                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(studentVM.FirstName.TrimStart().TrimEnd()),
                LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(studentVM.LastName.TrimStart().TrimEnd()),
                CNP = long.Parse(studentVM.CNP),
                Specialization = (int)studentVM.Specialization,
                YearOfStudy = (int)studentVM.YearOfStudy,
            };

            return student;
        }
    }
}