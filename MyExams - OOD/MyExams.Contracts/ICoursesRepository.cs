using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Dto;


namespace MyExams.Contracts
{
    public interface ICoursesRepository : IGenericRepository<CourseDto, CourseDetailsDto>, ISpecializedRepository<CourseDetailsDto>, 
                                          IGetByIdSpecialized<CourseDetailsDto>, IGenerateSelectList, IRelatedPerson<CourseDto>
    { 
    }
}
