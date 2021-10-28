using System.Web;
using System.Web.Mvc;

namespace DSCC._7417.Interface
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
