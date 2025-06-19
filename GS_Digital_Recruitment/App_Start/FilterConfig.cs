using System.Web;
using System.Web.Mvc;

namespace GS_Digital_Recruitment {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
