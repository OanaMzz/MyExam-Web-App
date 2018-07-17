using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExams.ViewModels
{
    public class PromoStudentSituationVM
    {
        public StudentVM Student { get; set; }

        public int ExamsCount { get; set; }

        public string ExamsToRetakeCount { get; set; }

        public string Average { get; set; }

        public string Credits { get; set; }
    }
}