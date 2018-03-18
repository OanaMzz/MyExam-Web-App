using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyExams.Enums;
using MyExams.Models;

namespace MyExams.ViewModels
{
    public class TeacherVM
    {
        public int TeacherId { get; set; }

        [Required()]
        [DisplayName("First Name")]
        [NameValidation(ErrorMessage = "First Name should begin and end only in letters")]
        [RegularExpression("^[a-zA-Z\\s-']+$", ErrorMessage = "Please insert only letters and  - ' ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name should be 2-50 characters")]
        public string FirstName { get; set; }

        [Required()]
        [DisplayName("Last Name")]
        [NameValidation(ErrorMessage = "First Name should begin and end only in letters")]
        [RegularExpression("^[a-zA-Z\\s-']+$", ErrorMessage = "Please insert only letters and  - ' ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name should be 2-50 characters")]
        public string LastName { get; set; }

        [DisplayName("Name")]
        public string TeacherFullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Required()]
        [DisplayName("Title")]
        public Title? TeacherTitle { get; set; }

        public List<CoursesOfTeacher> CoursesOfTeacher { get; set; }
    }
}