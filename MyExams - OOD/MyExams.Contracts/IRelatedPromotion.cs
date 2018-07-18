using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface IRelatedPromotion<U,C>
        where C : class // typeof(C) = MyExams.Dto - special case(get by promotion)
        where U : class // typeof(C) = MyExams.Dto - special case(students)
    {
        Dictionary<U, List<C>> GetRelatedByPromotion(int specialization, int year);
    }
}
