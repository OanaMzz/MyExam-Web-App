using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyExams.Models;
using MyExams.Enums;

namespace MyExams.ViewModels
{
    public class PromotionSituationVM
    {
        public List<PromotionSituationTable> PromotionSituationTable { get; set; }

        [DisplayName("Promotion Average")]
        public string PromotionAverage { get; set; }

        [DisplayName("Best situation")]
        public string HighestAverage { get; set; }

        public List<StudentDetails> Scholarships { get; set; }

        public Specialization Specialization { get; set; }

        public YearOfStudy YearOfStudy { get; set; }
    }
}