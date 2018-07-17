using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyExams.ViewModels
{
    public class ExamDetailsVM
    {
        public int ExamId { get; set; }

        public int CourseId { get; set; }

        [DisplayName("Course Name")]
        public string Name { get; set; }

        public StudentVM StudentDetails { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Grade { get; set; }

        public int Credits { get; set; }
    }
}