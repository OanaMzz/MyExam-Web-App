using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyExams.Models;
using System.Web.Mvc;

namespace MyExams.ViewModels
{
    public class ExamVM
    {
        public int ExamId { get; set; }

        [Required()]
        [DisplayName("Course")]
        public int? CourseId { get; set; }

        [Required()]
        [DisplayName("Student")]
        public int? StudentId { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime? Date { get; set; }

        [RegularExpression("^\\d*(\\.\\d{0,2})?$", ErrorMessage = "Please insert only 2 decimals")]
        public string Grade { get; set; }

        public SelectList CourseIdSelectList { get; set; }

        public SelectList StudentIdSelectList { get; set; }

    }
}