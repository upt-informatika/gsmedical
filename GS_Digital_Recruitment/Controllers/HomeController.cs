using GS_Digital_Recruitment.Models;
using GS_Digital_Recruitment.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GS_Digital_Recruitment.Controllers {
    public class HomeController : Controller {

        public GSDbContext GSDbContext { get; set; }
        public HomeController()
        {

            GSDbContext = new GSDbContext("10.5.0.123", "DB_GSRECRUITMENT","sa", "Brekele893");
        }
        protected override void Dispose(bool disposing)
        {
            GSDbContext.Dispose();
        }
        string FormatNumber<T>(T number, int maxDecimals = 4)
        {
            return Regex.Replace(String.Format("{0:n" + maxDecimals + "}", number),
                                 @"[" + System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator + "]?0+$", "");
        }

        [SessionCheck]
        public ActionResult Index() {
            return View("~/Views/Home/Index.cshtml");
        }

    }
}