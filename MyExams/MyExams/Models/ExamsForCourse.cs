using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyExams.Models
{
    public class ExamsForCourse
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Date { get; set; }

        public StudentDetails Student { get; set; }

        public StudentProfile StudentProfile { get; set; }

        public string Grade { get; set; }

        public double? GradeToConvert { get; set; }
    }
}