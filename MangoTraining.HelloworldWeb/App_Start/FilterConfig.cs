using System.Web;
using System.Web.Mvc;

namespace MangoTraining.HelloworldWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
