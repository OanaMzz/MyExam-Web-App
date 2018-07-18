using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyExams.Infrastructure;
using System.Web.Mvc;

namespace MyExams.ViewModels
{
    public class SituationFormVM
    {
        [DisplayName("Student Name")]
        public int? StudentId { get; set; }

        public SelectList StudentIdSelectList { get; set; }

        public Specialization? Specialization { get; set; }

        [DisplayName("Year of Study")]
        public YearOfStudy? YearOfStudy { get; set; }
    }
}