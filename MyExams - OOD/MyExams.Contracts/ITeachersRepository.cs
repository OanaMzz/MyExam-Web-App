using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Dto;

namespace MyExams.Contracts
{
    public interface ITeachersRepository : IGenericRepository<TeacherDto,TeacherDto>, ISpecializedRepository<TeacherDto>, IGenerateSelectList
    {
    }
}
