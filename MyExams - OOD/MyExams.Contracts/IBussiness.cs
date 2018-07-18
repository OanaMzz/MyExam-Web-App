using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyExams.Dto;

namespace MyExams.Contracts
{
    public interface IBussiness
    {
        StudentSituationDto GetStudentSituation(List<ExamsOfStudentDto> relatedExams, StudentsDto studentInfo);

        PromotionSituationDto GetPromotionSituation(Dictionary<StudentsDto, List<ExamsOfStudentDto>> promoStudentSituations, int specialization, int year);
    }
}
