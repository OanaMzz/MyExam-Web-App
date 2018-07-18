using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface IGetByIdSpecialized<C>
        where C : class // typeof(C) = MyExams.Dto - special case(get more details)
    {
        C GetDetailsById(int id);

        
    }
}
