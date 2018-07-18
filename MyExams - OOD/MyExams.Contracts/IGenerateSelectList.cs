using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyExams.Contracts
{
    public interface IGenerateSelectList
    {
        SelectList GetSelectList(int? id = null);
    }
}
