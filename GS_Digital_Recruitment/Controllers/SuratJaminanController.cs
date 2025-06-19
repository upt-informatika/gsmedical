using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using GS_Digital_Recruitment.Models;
using GS_Digital_Recruitment.Utils;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;

namespace GS_Digital_Recruitment.Controllers
{
    public class SuratJaminanController : ApiController
    {
        public GSDbContext GSDbContext { get; set; }
        private SessionLogin sessionLogin = (SessionLogin)System.Web.HttpContext.Current.Session["SHealth"];

        public SuratJaminanController()
        {
            GSDbContext = new GSDbContext("REVALINA", "db_gsmedical", "sa", "polman");
        }

        protected override void Dispose(bool disposing)
        {
            GSDbContext.Dispose();
        }

        [SessionCheck]
        [HttpGet]
        public HttpResponseMessage Get(
                 DataSourceLoadOptions loadOptions,
                 DateTime? tglAwal = null,
                 DateTime? tglAkhir = null,
                 string plant = null,
                 string status = null,
                 string jenis = null)
                    {
            try
            {
                IQueryable<TransaksiSuratJaminan> query = GSDbContext.TransaksiSuratJaminan;

                if (tglAwal.HasValue)
                    query = query.Where(x => x.psj_tanggal_pengajuan >= tglAwal.Value);

                if (tglAkhir.HasValue)
                    query = query.Where(x => x.psj_tanggal_pengajuan <= tglAkhir.Value);

               /* if (!string.IsNullOrEmpty(plant))
                    query = query.Where(x => x.plant == plant);*/

                if (!string.IsNullOrEmpty(status))
                    query = query.Where(x => x.psj_status == status);

                if (!string.IsNullOrEmpty(jenis))
                    query = query.Where(x => x.psj_jenis_surat_jaminan == jenis);

                var result = DataSourceLoader.Load(query, loadOptions);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        [SessionCheck]
        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form)
        {
            try
            {
                var values = form.Get("values");
                var entity = new TransaksiSuratJaminan();

                JsonConvert.PopulateObject(values, entity);

                Validate(entity);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

                entity.psj_created_date = DateTime.UtcNow.AddHours(7);
                entity.psj_created_by = "Revalina"; // atau sessionLogin.fullname;

                GSDbContext.TransaksiSuratJaminan.Add(entity);
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
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Key tidak dikirim.");

                var key = Convert.ToInt32(keyStr);
                var values = form.Get("values");

                var entity = GSDbContext.TransaksiSuratJaminan.FirstOrDefault(e => e.psj_id == key);
                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data tidak ditemukan.");

                JsonConvert.PopulateObject(values, entity);

                Validate(entity);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.GetFullErrorMessage());

                entity.psj_modif_date = DateTime.UtcNow.AddHours(7);
                entity.psj_modif_by = "Revalina"; // atau sessionLogin.fullname;

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
                var key = Convert.ToInt32(form.Get("key"));
                var entity = GSDbContext.TransaksiSuratJaminan.FirstOrDefault(e => e.psj_id == key);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data tidak ditemukan.");

                GSDbContext.TransaksiSuratJaminan.Remove(entity);
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
