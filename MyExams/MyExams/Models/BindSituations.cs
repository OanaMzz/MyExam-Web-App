using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyExams.Models
{
    public class StudentSituation
    {
        public int StudentId { get; set; }

        public int Specialization { get; set; }

        public int YearOfStudy { get; set; }
    }

    public class PromotionSituation
    {
        public int Specialization { get; set; }

        public int YearOfStudy { get; set; }
    }
}