using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.Common.Entities;
using TimeManager.BL.Entities;
using TimeManager.WebApp.Http;

namespace TimeManager.WebApp.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            var ListCliente = ClienteBL.GetList();
            return View(ListCliente);
        }

        public ActionResult Details(int id)
        {
            Cliente Cliente = ClienteBL.GetById(id);
            if (Cliente != null)
            {
                return View(Cliente);
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
        public ActionResult Create(Cliente cliente)
        {
            cliente.EsActivo = true;
            cliente.FechaRegistro = DateTime.Now;

            cliente = ClienteBL.Create(cliente);
            if (cliente.Id > 0)
            {

                return RedirectToAction("Details", new { id = cliente.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al ingresar el cliente");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool IsDelete = ClienteBL.Delete(id);

            JsonResponse JsonResponse = new JsonResponse()
            {
                IsSuccess = IsDelete
            };
            return Json(JsonResponse);
        }
    }
}