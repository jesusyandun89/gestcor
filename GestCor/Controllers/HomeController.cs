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
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        [CustomAuthorizeAttribute(Roles = "Home-Leer")]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}