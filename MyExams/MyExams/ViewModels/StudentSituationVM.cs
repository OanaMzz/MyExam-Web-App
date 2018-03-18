using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Models;
using System.ComponentModel;


namespace MyExams.ViewModels
{
    public class StudentSituationVM
    {
        public string Average { get; set; }

        [DisplayName("Credits Accumulated")]
        public string Credits { get; set; }

        [DisplayName("Exams to Retake")]
        public List<string> ExamsToRetake { get; set; }

        public int ExamsCount { get; set; }

        public int ExamsToRetakeCount { get; set; }

        public StudentDetails StudentName { get; set; }

        public StudentProfile StudentProfile { get; set; }

    }
}