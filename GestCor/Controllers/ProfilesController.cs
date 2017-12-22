using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class ProfilesController : Controller
    {
        // GET: Profiles
        public ActionResult Index()
        {
            GestCorProfile Profile = new GestCorProfile();

            return View(Profile.GetProfiles());
        }

        // GET: Profiles/Create
        public ActionResult Create()
        {
            try
            { 
            Module module = new Module();
            Rol rol = new Rol();

            var moduleList = module.getModulesAvaliable();
            var rolList = rol.getRolesAvaliable();

            var listMod = new SelectList(moduleList, "Id", "NameModule");
            var listRol = new SelectList(rolList, "Id", "NameRol");

            ViewData["modules"] = listMod;
            ViewData["roles"] = listRol;
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: Profiles/Create
        [HttpPost]
        public ActionResult Create(GestCorProfile model, int modules, int roles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.RolId = roles;
                model.IdModule = modules;
                if (model.SaveProfile())
                    return RedirectToAction("Index");
                else
                    return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Profiles/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                GestCorProfile profile = new GestCorProfile();
                Module module = new Module();
                Rol rol = new Rol();

                var moduleList = module.getModulesAvaliable();
                var rolList = rol.getRolesAvaliable();

                var listMod = new SelectList(moduleList, "Id", "NameModule");
                var listRol = new SelectList(rolList, "Id", "NameRol");

                ViewData["modules"] = listMod;
                ViewData["roles"] = listRol;

                return View(profile.GetProfilesById(id));
            }
            catch
            {
                return View("Error");
            }
            
            
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GestCorProfile model, int modules, int roles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.RolId = roles;
                model.IdModule = modules;
                if (model.UpdateProfile(id))
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
