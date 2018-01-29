using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;

namespace TimeManager.WebApp.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Index()
        {
            var ListProyecto = ProyectoBL.GetList();
            return View(ListProyecto);
        }

        public ActionResult Details(int id)
        {
            Proyecto Proyecto = ProyectoBL.GetById(id);
            if (Proyecto != null)
            {
                return View(Proyecto);
            }
            else
            {
                // Alerta de error
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proyecto proyecto)
        {
            proyecto.EsActivo = true;
            proyecto.FechaRegistro = DateTime.Now;

            proyecto = ProyectoBL.Create(proyecto);
            if (proyecto.Id > 0)
            {

                return RedirectToAction("Details", new { id = proyecto.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al ingresar el proyecto");
                return View();
            }
        }
    }
}