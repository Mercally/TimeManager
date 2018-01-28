using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;

namespace TimeManager.WebApp.Controllers
{
    public class UsuarioController : Controller
    {        
        public ActionResult Index()
        {
            List<Usuario> ListUsuario = UsuarioBL.GetList();
            return View(ListUsuario);
        }

        public ActionResult Details(int id)
        {
            Usuario Usuario = UsuarioBL.GetById(id);
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
            Usuario Usuario = UsuarioBL.Create(usuario);
            return RedirectToAction("Details", new { id = usuario.Id });
        }

        public ActionResult Edit(int id)
        {
            Usuario Usuario = UsuarioBL.GetById(id);
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
    }
}