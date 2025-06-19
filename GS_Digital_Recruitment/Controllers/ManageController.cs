using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using GS_Digital_Recruitment.Models;
using GS_Digital_Recruitment.Utils;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using GS_Digital_Recruitment.Controllers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GS_Digital_Recruitment.Controllers
{
    public class ManageController : Controller
    {
        public GSDbContext GSDbContext { get; set; }
        private SessionLogin sessionLogin = (SessionLogin)System.Web.HttpContext.Current.Session["SHealth"];

        public ManageController()
        {

            GSDbContext = new GSDbContext(".", "db_marketing_portal", "sa", "polman");
        }

        protected override void Dispose(bool disposing)
        {
            GSDbContext.Dispose();
        }

       
        public ActionResult MasterUserRecruiter()
        {
            return View();
        }
        public ActionResult MasterDivision()
        {
            return View();
        }

        public ActionResult IndexAdmin()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult ListManageForecast()
        {
            return View();
        }

        public ActionResult ListSuratJaminan() 
        { return View(); }

    }
}