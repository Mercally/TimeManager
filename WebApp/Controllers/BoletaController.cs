using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;
using TimeManager.WebApp.Http;
using TimeManager.Common.Enums;
using TimeManager.Common.Methods;

namespace TimeManager.WebApp.Controllers
{
    public class BoletaController : Controller
    {
        public ActionResult Index()
        {
            var ListBoleta = BoletaBL.GetList(false);
            return View(ListBoleta);
        }

        public ActionResult Create()
        {
            var ListCliente = ClienteBL.GetList();
            ViewBag.ListCliente = Catalogo.GetSelectListFromCatalog(ListCliente.Select(x => new Catalogo() { Id = x.Id, Nombre = x.Nombre }).ToList());

            var ListProyecto = ProyectoBL.GetList();
            ViewBag.ListProyecto = Catalogo.GetSelectListFromCatalog(ListProyecto.Select(x => new Catalogo() { Id = x.Id, Nombre = x.Nombre }).ToList());

            var ListTiempoInvertido = CatalogoBL.GetList(CatalogoEnum.TiempoInvertido);
            ViewBag.ListTiempoInvertido = Catalogo.GetSelectListFromCatalog(ListTiempoInvertido);

            var ListDepartamento = CatalogoBL.GetList(CatalogoEnum.Departamento);
            ViewBag.Departamento = Catalogo.GetSelectListFromCatalog(ListDepartamento);

            return View();
        }

        [HttpPost]
        public ActionResult Create(Boleta boleta)
        {
            boleta.FechaRegistro = DateTime.Now;
            boleta.EsActivo = true;
            boleta.UsuarioId = 1; // Usuario en sesion

            boleta = BoletaBL.Create(boleta);
            if (boleta.Id > 0)
            {
                return RedirectToAction("Create", "Actividad", new { id = boleta.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al ingresar la boleta");
                return View(boleta);
            }
        }

        public ActionResult Edit(int id)
        {
            Boleta Boleta = BoletaBL.GetById(id);

            if (Boleta != null)
            {
                var ListCliente = ClienteBL.GetList();
                ViewBag.ListCliente = Catalogo.GetSelectListFromCatalog(ListCliente.Select(x => new Catalogo() { Id = x.Id, Nombre = x.Nombre }).ToList());

                var ListProyecto = ProyectoBL.GetList();
                ViewBag.ListProyecto = Catalogo.GetSelectListFromCatalog(ListProyecto.Select(x => new Catalogo() { Id = x.Id, Nombre = x.Nombre }).ToList());

                var ListTiempoInvertido = CatalogoBL.GetList(CatalogoEnum.TiempoInvertido);
                ViewBag.ListTiempoInvertido = Catalogo.GetSelectListFromCatalog(ListTiempoInvertido);

                var ListDepartamento = CatalogoBL.GetList(CatalogoEnum.Departamento);
                ViewBag.Departamento = Catalogo.GetSelectListFromCatalog(ListDepartamento);

                return View(Boleta);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Boleta boleta)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool IsDelete = BoletaBL.Delete(id);

            JsonResponse JsonResponse = new JsonResponse()
            {
                IsSuccess = IsDelete
            };
            return Json(JsonResponse);
        }
    }
}