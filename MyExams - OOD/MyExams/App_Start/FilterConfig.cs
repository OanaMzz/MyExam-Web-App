using System.Web;
using System.Web.Mvc;
using MyExams.CustomExFilter;

namespace MyExams
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleGeneral());
        }
    }
}
