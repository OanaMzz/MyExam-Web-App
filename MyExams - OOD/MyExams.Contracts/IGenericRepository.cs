using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Contracts
{
    public interface IGenericRepository<C, U>
        where C : class // typeOf(C) = MyExams.Dto
        where U : class // typeOf(T) = MyExams.Dto - special case
    {
        C GetById(int id);

        void Add(C dto);

        void Edit(C dto);

        void Delete(C dto);

        string GetOrder(string sortOrder, ref string lastOrder);

        string GetSearchString(string searchString);

        void GetPagedListParameters(int? page, List<U> list, out int pageSize, out int pageNumber);
    }
}
