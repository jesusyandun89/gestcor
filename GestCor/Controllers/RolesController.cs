﻿using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Roles-Leer")]
        public ActionResult Index()
        {
            GestCorRol Rol = new GestCorRol();
            return View(Rol.GetRoles());
        }

        // GET: Roles/Create
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Roles-Crear")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Roles-Crear")]
        public ActionResult Create(GestCorRol model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.SaveGestCorRoles())
                {
                    TempData["AlertMessage"] = "ROL GUARDADO CON EXITO";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al crear el rol";
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Roles/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Roles-Editar")]
        public ActionResult Edit(int id)
        {
            GestCorRol rol = new GestCorRol();
            return View(rol.GetRolesById(id));
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Roles-Editar")]
        public ActionResult Edit(int id, GestCorRol model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (model.UpdateGestCorRoles(id))
                {
                    TempData["AlertMessage"] = "ROL EDITADO CON EXITO";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al editar el rol";
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
