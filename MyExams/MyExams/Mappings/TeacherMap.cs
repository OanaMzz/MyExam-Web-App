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
    public class TeacherMap
    {
        public static IEnumerable<TeacherVM> TeachersToTeachersVM(IEnumerable<Teacher> teachers)
        {
            var teachersVM = new List<TeacherVM>();

            foreach (var teacher in teachers)
            {
                var teacherVM = TeacherToTeacherVM(teacher);

                teachersVM.Add(teacherVM);
            }

            return teachersVM;
        }

        public static TeacherVM TeacherToTeacherVM(Teacher teacher)
        {
            var teacherVM = new TeacherVM
            {
                TeacherId = teacher.TeacherId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                TeacherTitle = (Title)teacher.Title,
            };

            return teacherVM;
        }

        public static Teacher TeacherVMToTeacher(TeacherVM teacherVM)
        {
            var teacher = new Teacher
            {
                TeacherId = teacherVM.TeacherId,
                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(teacherVM.FirstName.TrimStart().TrimEnd()),
                LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(teacherVM.LastName.TrimStart().TrimEnd()),
                Title = (int)teacherVM.TeacherTitle,
            };

            return teacher;

        }
    }
}