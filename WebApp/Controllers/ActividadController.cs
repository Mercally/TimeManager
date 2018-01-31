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

        // GET: Index/id:int -> actividadId
        public ActionResult Details(int id)
        {
            Actividad Actividad = ActividadBL.GetById(id);
            return View(Actividad);
        }

        // GET: Create/id:int -> boletaId
        public ActionResult Create(int id)
        {
            var ListEstadoActividad = CatalogoBL.GetList(CatalogoEnum.EstadoVisita);
            var ListEstadoActividadItems = ListEstadoActividad.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListEstadoActividadItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.ListEstadoActividad = new SelectList(ListEstadoActividadItems, "Value", "Text");

            Actividad model = new Actividad() { BoletaId = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Actividad actividad)
        {
            actividad.FechaRegistro = DateTime.Now;
            actividad.EsActivo = true;
            actividad = ActividadBL.Create(actividad);
            if (actividad.Id > 0)
            {
                ModelState.Clear();
                return RedirectToAction("Create", new { id = actividad.BoletaId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la actividad");
                return RedirectToAction("Create", new { id = actividad.BoletaId });
            }
        }

        // GET: Index/id:int -> actividadId
        public ActionResult Edit(int id)
        {
            var ListEstadoActividad = CatalogoBL.GetList(CatalogoEnum.EstadoVisita);
            var ListEstadoActividadItems = ListEstadoActividad.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListEstadoActividadItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.ListEstadoActividad = new SelectList(ListEstadoActividadItems, "Value", "Text");

            Actividad model = ActividadBL.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Actividad actividad)
        {
            bool IsUpdate = ActividadBL.Update(actividad);
            if (IsUpdate)
            {
                return RedirectToAction("Details", new { id = actividad.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al modificar la actividad");
                return RedirectToAction("Edit", new { id = actividad.BoletaId });
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="fecha"></param>
        /// <param name="tiempo"></param>
        /// <returns></returns>
        private string CombineDescription(string descripcion, DateTime fecha, decimal tiempo)
        {
            return $"{descripcion} - {fecha.ToShortDateString()} - {tiempo.ToString()} H";
        }
    }
}