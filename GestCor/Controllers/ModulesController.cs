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
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Modules-Leer")]
        public ActionResult Index()
        {
            GestCorModules Module = new GestCorModules();

            return View(Module.GetModules());
        }


        // GET: Modules/Create
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Modules-Crear")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Modules-Crear")]
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
                {
                    ModelState.AddModelError("", "Error en la creación del modulo..");
                    return View();
                }

            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Modules/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Modules-Editar")]
        public ActionResult Edit(int id)
        {
            GestCorModules Module = new GestCorModules();

            return View(Module.GetModulesById(id));
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Modules-Editar")]
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
                {
                    ModelState.AddModelError("", "Error en la edición del modulo..");
                    return View();
                }
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
