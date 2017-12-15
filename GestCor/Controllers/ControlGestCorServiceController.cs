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
        public ActionResult Index()
        {
            bool status = model.getStatus();
            ViewBag.Status = status;
            return View();
        }


        public async Task<ActionResult> Active()
        {
            ViewBag.SyncOrAsync = "Asynchronous";
            await model.run_async(true);
            ViewBag.Script = "Proceso iniciado correctamente";
            return View("Index");
        }


        public async Task<ActionResult> Desactive()
        {
            ViewBag.SyncOrAsync = "Asynchronous";
            await model.run_async(false);
            ViewBag.Script = "Proceso detenido correctamente";
            return View("Index");
        }
    }
}