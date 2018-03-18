using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyExams.ViewModels
{
    public class SituationVM
    {
        public StudentSituationFormVM StudentSituation { get; set; }

        public PromotionSituationFormVM PromotionStituation { get; set; }
    }


    public class StudentSituationFormVM
    {
        [Required()]
        [DisplayName("Student Name")]
        public int StudentId { get; set; }

        public SelectList StudentIdSelectList { get; set; }
    }

    public class PromotionSituationFormVM
    {
        [Required()]
        public Specialization? Specialization { get; set; }

        [Required()]
        [DisplayName("Year of Study")]
        public YearOfStudy? YearOfStudy { get; set; }
    }
}