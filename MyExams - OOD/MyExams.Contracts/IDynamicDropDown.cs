using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyExams.Contracts
{
    public interface IDynamicDropDown
    {
        SelectList GetCurrentSpecializations(int? id = null);

        SelectList GetYearForSpecialization(int specialization, int? id = null);

    }
}
