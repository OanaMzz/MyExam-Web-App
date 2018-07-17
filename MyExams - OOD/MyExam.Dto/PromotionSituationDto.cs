using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyExams.Dto
{
    public class PromotionSituationDto
    {
        public int Specialization { get; set; }

        public int YearOfStudy { get; set; }

        [DisplayName("Promotion Average")]
        public double? PromotionAverage { get; set; }

        [DisplayName("Best situation")]
        public double? HighestAverage { get; set; }

        public List<string> Scholarships { get; set; }

        public List<PromoStudentSituationDto> StudentSituationTable { get; set; }

    }
}
