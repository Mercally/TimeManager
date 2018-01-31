using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;
using TimeManager.Common.Methods;

namespace TimeManager.WebApp.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Boletas(DateTime? del = null, DateTime? hasta = null)
        {
            IEnumerable<Boleta> ListBoleta = null;
            if (del.HasValue && hasta.HasValue)
            {
                ListBoleta = BoletaBL.GetReporteFechas(Converting.DateTimeToSqlString(del.Value), Converting.DateTimeToSqlString(hasta.Value, 1));
            }
            else
            {
                ListBoleta = BoletaBL.GetReporteFechas(Formatting.SqlStringDateFirstDayCurrentMonth(), Formatting.SqlStringDateLastDayCurrentMonth(1));
            }
            return View(ListBoleta);
        }
    }
}