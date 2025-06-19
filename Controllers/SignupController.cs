using System;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using GS_Digital_Recruitment.Models;

namespace GS_Digital_Recruitment.Controllers
{
    public class SignupController : Controller
    {
        public GSDbContext GSDbContext { get; set; }

        public SignupController()
        {
            GSDbContext = new GSDbContext("10.5.0.123", "DB_GSRECRUITMENT","sa", "Brekele893");
        }

        protected override void Dispose(bool disposing)
        {
            GSDbContext.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
