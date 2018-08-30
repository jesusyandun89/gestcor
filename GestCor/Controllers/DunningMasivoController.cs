using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class DunningMasivoController : Controller
    {
        GenerarDunningMasivoModels GenerarDunningMasivo = new GenerarDunningMasivoModels();
        // GET: DunningMasivo
        public ActionResult Index()
        {
            List<GenerarDunningMasivo> GenerarDunningMasivoList = new List<GenerarDunningMasivo>();

            GenerarDunningMasivoList = GenerarDunningMasivo.getDunningMasivo();

            return View(GenerarDunningMasivoList);
        }

        // GET: DunningMasivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DunningMasivo/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DunningMasivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DunningMasivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
