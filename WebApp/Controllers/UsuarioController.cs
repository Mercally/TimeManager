using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;
using TimeManager.WebApp.Http;

namespace TimeManager.WebApp.Controllers
{
    public class UsuarioController : Controller
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
            List<Usuario> ListUsuario = UsuarioBL.GetList(false);
            return View(ListUsuario);
        }

        public ActionResult Details(int id)
        {
            Usuario Usuario = UsuarioBL.GetById(id, false);
            return View(Usuario);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            usuario.FechaRegistro = DateTime.Now;
            usuario.EsActivo = true;
            usuario = UsuarioBL.Create(usuario);

            if (usuario.Id > 0)
            {
                return RedirectToAction("Details", new { id = usuario.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al agregar el usuario");
                return View(usuario);
            }
        }

        public ActionResult Edit(int id)
        {
            Usuario Usuario = UsuarioBL.GetById(id, false);
            return View(Usuario);
        }

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (UsuarioBL.Update(usuario))
            {
                return RedirectToAction("Details", new { id = usuario.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al guardar el registro");
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool IsDelete = UsuarioBL.Delete(id);

            JsonResponse JsonResponse = new JsonResponse()
            {
                IsSuccess = IsDelete
            };
            return Json(JsonResponse);
        }
    }
}