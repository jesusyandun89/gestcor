using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class HomeController : Controller
    {
        
        [CustomAuthorizeAttribute(Roles = "Home-Leer")]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        [CustomAuthorizeAttribute(Roles = "Home-Leer")]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Sobre nosotros.";

            return View();
        }
        
        [CustomAuthorizeAttribute(Roles = "Home-Leer")]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto.";

            return View();
        }
    }
}