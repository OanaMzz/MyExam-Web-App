using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface IRelatedPerson<C>
        where C : class // typeof(C) = MyExam.Dto - special case(get related by student/teacher)
    {
        List<C> GetRelatedByPerson(int id);
    }
}
