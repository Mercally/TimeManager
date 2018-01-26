using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeManager.BL;
using TimeManager.Common.Transaction;
using TimeManager.Entities;

namespace TimesManager.WebApp.Controllers
{
    public class UsuarioController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(Usuario usuario)
        {
            using (Transaction Tran = new Transaction())
            {
                usuario.Id = Tran.Execute<int>(UsuarioBL.Create(usuario));
                
                Tran.Commit();
            }
            return View(usuario);
        }
    }
}