using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Dto;
using MyExams.Infrastructure;
using System.ComponentModel;

namespace MyExams.ViewModels
{
    public class PromotionSituationVM
    {
        public Specialization Specialization { get; set; }

        public YearOfStudy YearOfStudy { get; set; }

        [DisplayName("Promotion Average")]
        public string PromotionAverage { get; set; }

        [DisplayName("Best situation")]
        public string HighestAverage { get; set; }

        public List<string> Scholarships { get; set; }

        public List<PromoStudentSituationVM> StudentSituationTable { get; set; }
    }
}