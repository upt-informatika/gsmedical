using Microsoft.AspNetCore.Mvc;

namespace GSRecruitment.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ManageMasterUserRecruiter()
        {
            return View();
        }
    }
}
