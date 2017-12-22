using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            GestCorModules Module = new GestCorModules();

            return View(Module.GetModules());
        }


        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        public ActionResult Create(GestCorModules model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.SaveModule())
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int id)
        {
            GestCorModules Module = new GestCorModules();

            return View(Module.GetModulesById(id));
        }

        // POST: Modules/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GestCorModules model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.UpdateModule(id))
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
