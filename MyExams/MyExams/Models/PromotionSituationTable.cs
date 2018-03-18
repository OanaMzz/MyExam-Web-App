using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MyExams.Models
{
    public class PromotionSituationTable
    {
        public int StudentId { get; set; }

        public string Average { get; set; }

        [DisplayName("Credits Accumulated")]
        public string Credits { get; set; }

        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [DisplayName("Exams to Retake")]
        public string ExamsToRetakeCount { get; set; }

        public int ExamsCount { get; set; }
    }


    public class PromotionSituationTableAux
    {
        public int StudentId { get; set; }

        public double Average { get; set; }

        [DisplayName("Credits Accumulated")]
        public int Credits { get; set; }

        public StudentDetails StudentName { get; set; }

        public int ExamsToRetakeCount { get; set; }

        public int ExamsCount { get; set; }

    }
}