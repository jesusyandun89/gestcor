using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class ControlGestCorServiceController : Controller
    {
        ControlGestCorService model = new ControlGestCorService();
        // GET: ControlGestCorService
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ControlGestCorService-Leer")]
        public ActionResult Index()
        {
            string status = model.getStatus();
            if (status == "El servicio se encuentra detenido")
            {
                ViewBag.Status = "false";
            }
            else if (status == "El servicio se encuentra ejecutandose con normalidad")
            {
                ViewBag.Status = "true";
            }
            else
            {
                ViewBag.Status = "-1";
            }
            
            return View();
        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ControlGestCorService-Editar")]
        public async Task<ActionResult> Active()
        {
            ViewBag.SyncOrAsync = "Asynchronous";
            await model.run_async(true);
            ViewBag.Status = "true";
            ViewBag.Script = "Proceso iniciado correctamente";
            return View("Index");
        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ControlGestCorService-Editar")]
        public async Task<ActionResult> Desactive()
        {
            ViewBag.SyncOrAsync = "Asynchronous";
            await model.run_async(false);
            ViewBag.Status = "false";
            ViewBag.Script = "Proceso detenido correctamente";
            return View("Index");
        }
    }
}