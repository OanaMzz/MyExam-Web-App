using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Models;
using MyExams.Entities;
using MyExams.Enums;
using MyExams.ViewModels;
using System.Web.Mvc;

namespace MyExams.Repositories
{
    public class SelectListHelper
    {

        public static MyExamsEntities db = new MyExamsEntities();

        public static List<SelectListItem> PopulateTeacherSelectList()
        {
            var teachers = db.Teachers.ToList().OrderBy(t => t.FirstName).ThenBy(t => t.LastName);

            var selectList = new List<SelectListItem>();

            foreach (var teacher in teachers)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = teacher.FirstName.ToString() + " " + teacher.LastName.ToString(),
                    Value = teacher.TeacherId.ToString()
                };

                selectList.Add(selectListItem);
            };

            return selectList;
        }


        public static List<SelectListItem> PopulateStudentsSelectList()
        {
            var students = db.Students.ToList().OrderBy(s => s.FirstName).ThenBy(s => s.LastName);

            var selectList = new List<SelectListItem>();

            foreach (var student in students)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = student.FirstName.ToString() + " " + student.LastName.ToString(),
                    Value = student.StudentId.ToString()
                };

                selectList.Add(selectListItem);
            };

            return selectList;
        }


        public static SelectList CreateCourseSelectList()
        {
            return new SelectList(db.Courses, "CourseId", "Name");
        }


        public static SelectList CreateCourseSelectList(int id)
        {
            return new SelectList(db.Courses, "CourseId", "Name", id);
        }


        public static SelectList CreateSelectListProp(List<SelectListItem> selectList)
        {
            return new SelectList(selectList, "Value", "Text");
        }


        public static SelectList CreateSelectListProp(List<SelectListItem> selectList, int id)
        {
            return new SelectList(selectList, "Value", "Text", id);
        }

    }


}





