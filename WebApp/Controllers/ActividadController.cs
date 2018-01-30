using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;
using TimeManager.Common.Enums;
using TimeManager.WebApp.Http;

namespace TimeManager.WebApp.Controllers
{
    public class ActividadController : Controller
    {
        // GET: Index/id:int -> boletaId
        public ActionResult Index(int id)
        {
            var ListActividad = ActividadBL.GetList(id);

            return View(ListActividad);
        }

        // GET: Create/id:int -> boletaId
        public ActionResult Create(int id)
        {
            Boleta Boleta = BoletaBL.GetById(id);
            if (Boleta != null)
            {
                var ListEstadoActividad = CatalogoBL.GetList(CatalogoEnum.EstadoVisita);
                var ListEstadoActividadItems = ListEstadoActividad.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
                ListEstadoActividadItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
                ViewBag.ListEstadoActividad = new SelectList(ListEstadoActividadItems, "Value", "Text");

                Actividad model = new Actividad() { BoletaId = id };
                return View(model);
            }
            else
            {
                return RedirectToAction("Edit", "Boleta", new { id });
            }
        }

        [HttpPost]
        public ActionResult Create(Actividad actividad)
        {
            actividad.FechaRegistro = DateTime.Now;
            actividad.EsActivo = true;
            actividad.Descripcion = CombineDescription(actividad.Descripcion, actividad.FechaActividad, actividad.TiempoActividad);
            actividad = ActividadBL.Create(actividad);
            if (actividad.Id > 0)
            {
                ModelState.Clear();
                return RedirectToAction("Create", new { id = actividad.BoletaId });
            }
            else
            {
                return RedirectToAction("Create", new { id = actividad.BoletaId });
            }
        }

        public ActionResult Edit(int boletaId, int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool IsDelete = ActividadBL.Delete(id);

            JsonResponse JsonResponse = new JsonResponse()
            {
                IsSuccess = IsDelete
            };
            return Json(JsonResponse);
        }

        private string CombineDescription(string descripcion, DateTime fecha, decimal tiempo)
        {
            return $"{descripcion} - {fecha.ToShortDateString()} - {tiempo.ToString()} H";
        }
    }
}