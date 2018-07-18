using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface ISpecializedRepository<C>
        where C : class // typeOf(T) = MyExams.Dto - special case
    {
        List<C> GetAll();

        List<C> GetSearch(string sortOrder, string searchString);
    }
}
