using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyExams.Models
{
    public class ExamsOfStudent
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime Date { get; set; }

        public CourseDetails Course { get; set; }

        public string Grade { get; set; }

        public double? GradeToConvert { get; set; }
    }
}