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
using System.Web.UI.WebControls;

namespace GS_Digital_Recruitment.Controllers
{
    public class ManageMasterUserRecruiterController : ApiController
    {
        public GSDbContext GSDbContext { get; set; }
        private SessionLogin sessionLogin = (SessionLogin)System.Web.HttpContext.Current.Session["SHealth"];

        public ManageMasterUserRecruiterController()
        {

            GSDbContext = new GSDbContext("10.5.0.123", "DB_GSRECRUITMENT","sa", "Brekele893");
        }

        protected override void Dispose(bool disposing)
        {
            GSDbContext.Dispose();
        }

        [SessionCheck]
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var dataList = GSDbContext.MasterUserRecruiter.ToList();
                return Request.CreateResponse(DataSourceLoader.Load(dataList, loadOptions));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [SessionCheck]
        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form)
        {
            try
            {
                var values = form.Get("values");
                var master = new MasterUserRecruiter();
                var noIDCust = "";

                JsonConvert.PopulateObject(values, master);

                Validate(master);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

                if (!string.IsNullOrEmpty(values) && values.Contains("prk_password"))
                    master.prk_password = Helper.EncodePassword(master.prk_password, "bangcakrek");

                master.prk_created_date = DateTime.UtcNow.AddHours(7);
                /*master.prk_created_by = sessionLogin.fullname;*/
                master.prk_created_by = "Revalina";

                GSDbContext.MasterUserRecruiter.Add(master);
                GSDbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [SessionCheck]
        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form)
        {
            try
            {
                var keyStr = form.Get("key");
                if (string.IsNullOrEmpty(keyStr))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key tidak dikirim.");
                }
                var key = Convert.ToInt64(keyStr);
                var values = form.Get("values");
                var master = GSDbContext.MasterUserRecruiter.First(e => e.prk_id == key);

                JsonConvert.PopulateObject(values, master);

                Validate(master);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

                if (!string.IsNullOrEmpty(values) && values.Contains("prk_password"))
                    master.prk_password = Helper.EncodePassword(master.prk_password, "bangcakrek");
                master.prk_modif_date = DateTime.UtcNow.AddHours(7);
                /*master.prk_modif_by = sessionLogin.fullname;*/
                master.prk_modif_by = "RevalinaNaufal";
                GSDbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        [SessionCheck]
        [HttpDelete]
        public HttpResponseMessage Delete(FormDataCollection form)
        {
            try
            {
                var key = Convert.ToInt64(form.Get("key"));
                var order = GSDbContext.MasterUserRecruiter.First(e => e.prk_id== key);

                GSDbContext.MasterUserRecruiter.Remove(order);
                GSDbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }
    }
}