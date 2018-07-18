using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyExams.ViewModels
{
    public class ExamsOfStudentVM
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DisplayName("Course Name")]
        public string Name { get; set; }

        public TeacherVM Teacher { get; set; }

        public string Grade { get; set; }

        public int Credits { get; set; }
    }
}