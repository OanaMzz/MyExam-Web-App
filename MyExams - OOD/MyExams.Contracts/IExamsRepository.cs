using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Dto;

namespace MyExams.Contracts
{
    public interface IExamsRepository : IGenericRepository<ExamDto, ExamDetailsDto>, ISpecializedRepository<ExamDetailsDto>, 
                                        IGetByIdSpecialized<ExamDetailsDto>, IRelatedPerson<ExamsOfStudentDto>,
                                        IRelatedPromotion<StudentsDto,ExamsOfStudentDto>, IRelatedCourse<ExamDetailsDto>
    {
    }
}
