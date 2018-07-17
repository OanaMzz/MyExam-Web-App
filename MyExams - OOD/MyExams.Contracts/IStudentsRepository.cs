using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Dto;

namespace MyExams.Contracts
{
    public interface IStudentsRepository : IGenericRepository<StudentsDto,StudentsDto>, ISpecializedRepository<StudentsDto>, IGenerateSelectList, IDynamicDropDown
    {
    }
}
