using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyExams.Models;
using System.Web.Mvc;

namespace MyExams.ViewModels
{
    public class CourseVM
    {

        public int CourseId { get; set; }

        [Required()]
        [DisplayName("Course")]
        [RegularExpression("^[a-zA-Z0-9,\\-.:() ]*$", ErrorMessage = "Please insert only letters, numbers and : . - , ( )")]
        [CourseNameValidation(ErrorMessage = "Course should begin only in letters and end only in letters, numbers or paranthesis")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Course should be 2-50 characters")]
        public string Name { get; set; }

        [Required()]
        [DisplayName("Teacher")]
        public int TeacherId { get; set; }

        [DisplayName("Duration (Semesters)")]
        [Required()]
        [Range(1,3, ErrorMessage = "Duration should be 1-3 Semesters")]
        public int? Duration { get; set; }

        [Required()]
        [Range(10,50, ErrorMessage = "Credits should be 10-50")]
        public int? Credits { get; set; }

        public List<ExamsForCourse> ExamsForCourse { get; set; }

        public TeacherDetails Teacher { get; set; }

        public SelectList TeacherIdSelectList { get; set; }

    }

}