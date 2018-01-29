using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;
using TimeManager.WebApp.Http;
using TimeManager.Common.Enums;

namespace TimeManager.WebApp.Controllers
{
    public class BoletaController : Controller
    {
        [HttpPost]
        public JsonResult RequestHandler(JsonRequest jsonRequest)
        {
            JsonResponse JsonResponse = new JsonResponse()
            {
                Modal = new Modal()
                {
                    Ajax = new Ajax()
                    {
                        UpdateElementId = $"DivFor{jsonRequest.Action}",
                        Url = Url.Action(jsonRequest.Action, jsonRequest.Controller, new { id = jsonRequest.Id })
                    },
                    OpenModalId = $"{jsonRequest.Action}Modal"
                },
                IsSuccess = true
            };
            return Json(JsonResponse);
        }

        public ActionResult Index()
        {
            var ListBoleta = BoletaBL.GetList(false);
            return View(ListBoleta);
        }

        public ActionResult Create()
        {
            var ListCliente = ClienteBL.GetList();
            var ListClienteItems = ListCliente.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListClienteItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.ListCliente = new SelectList(ListClienteItems, "Value", "Text");

            var ListProyecto = ProyectoBL.GetList();
            var ListProyectoItems = ListProyecto.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListProyectoItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.ListProyecto = new SelectList(ListProyectoItems, "Value", "Text");

            var ListTiempoInvertido = CatalogoBL.GetList(CatalogoEnum.TiempoInvertido);
            var ListTiempoInvertidoItems = ListTiempoInvertido.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListTiempoInvertidoItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.ListTiempoInvertido = new SelectList(ListTiempoInvertidoItems, "Value", "Text");

            var ListDepartamento = CatalogoBL.GetList(CatalogoEnum.Departamento);
            var ListDepartamentoItems = ListDepartamento.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();
            ListDepartamentoItems.Insert(0, new SelectListItem() { Text = "Seleccione", Value = "0" });
            ViewBag.Departamento = new SelectList(ListDepartamentoItems, "Value", "Text");

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
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Boleta boleta)
        {
            return View();
        }
    }
}