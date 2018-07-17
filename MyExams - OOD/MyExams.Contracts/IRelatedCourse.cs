using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface IRelatedCourse<C>
        where C : class // typeof(C) = MyExams.Dto - special case(get related by course)
    {
        List<C> GetRelatedByCourse(int id);
    }
}
