using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Dto;
using System.ComponentModel;

namespace MyExams.ViewModels
{
    public class StudentSituationVM
    {
        public StudentVM Student { get; set; }

        public int ExamsCount { get; set; }

        public int ExamsToRetakeCount { get; set; }

        [DisplayName("Exams to Retake")]
        public List<string> ExamsToRetake { get; set; }

        public string Average { get; set; }

        [DisplayName("Credits Accumulated")]
        public string Credits { get; set; }

        public List<ExamsOfStudentVM> ExamTable { get; set; } 
     
    }
}