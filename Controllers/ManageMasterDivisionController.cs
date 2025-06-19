using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using GS_Digital_Recruitment.Controllers;
using GS_Digital_Recruitment.Models;
using GS_Digital_Recruitment.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace GS_Digital_Recruitment.Controllers
{
    public class ManageMasterDivisionController : ApiController
    {
        public GSDbContext GSDbContext { get; set; }
        private SessionLogin sessionLogin = (SessionLogin)System.Web.HttpContext.Current.Session["SHealth"];

        public ManageMasterDivisionController()
        {

            GSDbContext = new GSDbContext("10.5.0.123", "DB_GSRECRUITMENT", "sa", "Brekele893");
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
                var dataList = GSDbContext.MasterDivision.ToList();
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
                var master = new MasterDivision();
                var noIDCust = "";

                JsonConvert.PopulateObject(values, master);

                Validate(master);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

                //if (!string.IsNullOrEmpty(values) && values.Contains("prk_password"))
                //    master.prk_password = Helper.EncodePassword(master.prk_password, "bangcakrek");

                master.div_createDate = DateTime.UtcNow.AddHours(7);
                /*master.prk_created_by = sessionLogin.fullname;*/
                master.div_createBy = "Agezz";
                master.div_status = true;

                GSDbContext.MasterDivision.Add(master);
                GSDbContext.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [HttpPut]
        [ActionName("CustomDelete")]
        public HttpResponseMessage CustomDelete(FormDataCollection form)
        {
            try
            {
                var key = Convert.ToInt64(form.Get("key")); // ambil ID primary key
                var data = GSDbContext.MasterDivision.FirstOrDefault(e => e.div_id == key);

                if (data != null)
                {
                    // Toggle status: jika true jadi false, jika false jadi true
                    data.div_status = !data.div_status;
                    data.div_modifDate = DateTime.Now; // opsional
                    data.div_modifBy = "Agezz"; // opsional

                    GSDbContext.SaveChanges();

                    var statusText = (data.div_status ?? false) ? "Aktif" : "Tidak Aktif";

                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        message = "Status berhasil diubah menjadi " + statusText
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Data tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

       

        //[SessionCheck]
        //[HttpDelete]
        //public HttpResponseMessage Delete(FormDataCollection form)
        //{
        //    try
        //    {
        //        var key = Convert.ToInt64(form.Get("key"));
        //        var order = GSDbContext.MasterDivision.First(e => e.div_id == key);

        //        GSDbContext.MasterDivision.Remove(order);
        //        GSDbContext.SaveChanges();

        //        return Request.CreateResponse(HttpStatusCode.OK);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
        //    }

        //}
    }
}

