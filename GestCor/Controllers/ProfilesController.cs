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
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Profiles-Leer")]
        public ActionResult Index()
        {
            GestCorProfile Profile = new GestCorProfile();

            return View(Profile.GetProfiles());
        }

        // GET: Profiles/Create
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Profiles-Crear")]
        public ActionResult Create()
        {
            try
            { 
                GestCorProfile profile = new GestCorProfile();

                List<SelectListItem> listMod = profile.getModulesAvaliable(profile.IdModule);
                List<SelectListItem> listRol = profile.getRolesAvaliable(profile.RolId);

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
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Profiles-Crear")]
        public ActionResult Create(GestCorProfile model, int modules, int roles)
        {
            if (!ModelState.IsValid || modules == 0 || roles == 0)
            {
                return View(model);
            }
            try
            {
                model.RolId = roles;
                model.IdModule = modules;
                if (model.SaveProfile())
                {
                    TempData["AlertMessage"] = "Perfil creado exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al crear el perfil";
                    return View("Error");
                }
                    
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Profiles/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Profiles-Editar")]
        public ActionResult Edit(int id)
        {
            try
            {
                GestCorProfile profile = new GestCorProfile();

                profile= profile.GetProfilesById(id);

                List<SelectListItem> listMod = profile.getModulesAvaliable(profile.IdModule);
                List<SelectListItem> listRol = profile.getRolesAvaliable(profile.RolId);

                ViewData["modules"] = listMod;
                ViewData["roles"] = listRol;

                return View(profile);
            }
            catch
            {
                return View("Error");
            }
            
            
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Profiles-Editar")]
        public ActionResult Edit(int id, GestCorProfile model, int modules, int roles)
        {
            if (!ModelState.IsValid || modules == 0 || roles == 0)
            {
                return View(model);
            }
            try
            {
                model.RolId = roles;
                model.IdModule = modules;
                if (model.UpdateProfile(id))
                {
                    TempData["AlertMessage"] = "Perfil editado exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al editar el perfil";
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
