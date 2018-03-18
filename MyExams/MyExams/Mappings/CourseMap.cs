using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using MyExams.ViewModels;
using MyExams.Repositories;
using System.Globalization;

namespace MyExams.Mappings
{
    public class CourseMap
    {
        public static IEnumerable<CourseVM> CoursesToCoursesVM(IEnumerable<Cours> courses)
        {
            var coursesVM = new List<CourseVM>();

            foreach (var course in courses)
            {
                var courseVM = CourseToCourseVM(course);

                coursesVM.Add(courseVM);
            }

            return coursesVM;
        }

        public static CourseVM CourseToCourseVM(Cours course)
        {
            var courseRepo = new CoursesRepository();

            var courseVM = new CourseVM
            {
                CourseId = course.CourseId,
                Name = course.Name,
                TeacherId = course.TeacherId,
                Duration = course.Duration,
                Credits = course.Credits, 
                Teacher = courseRepo.GetTeacherDetails(course.CourseId)
            };

            return courseVM;
        }

        public static Cours CourseVMToCourse(CourseVM courseVM)
        {
            var course = new Cours
            {
                CourseId = courseVM.CourseId,
                Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(courseVM.Name.TrimStart().TrimEnd()),
                TeacherId = courseVM.TeacherId,
                Duration = (int)courseVM.Duration,
                Credits = (int)courseVM.Credits,
            };

            return course;
        }
    }
}