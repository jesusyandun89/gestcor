﻿using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class CorreoNotificacionesController : Controller
    {
        // GET: CorreoNotificaciones
        public ActionResult Index()
        {
            Ytbl_CorreoNotificacionesModels SelectCorreos = new Ytbl_CorreoNotificacionesModels();

            return View(SelectCorreos.SelectCorreos());
        }

        // GET: CorreoNotificaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorreoNotificaciones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Ytbl_CorreoNotificacionesModels saveDestinatario = new Ytbl_CorreoNotificacionesModels();

                saveDestinatario.Name = collection[1].ToString();
                saveDestinatario.Correo = collection[2].ToString();
                saveDestinatario.IsValid = collection[3].ToString();
                saveDestinatario.System = "GestCor";
                saveDestinatario.Fecha = DateTime.Now;

                if (saveDestinatario.SaveYtbl_CorreoNotificaciones(saveDestinatario))
                    return RedirectToAction("Index");
                else
                    return View("Error");

            }
            catch
            {
                return View("Error");
            }
        }

        // GET: CorreoNotificaciones/Edit/5
        public ActionResult Edit(int id)
        {
            Ytbl_CorreoNotificacionesModels correoNotificacion = new Ytbl_CorreoNotificacionesModels();

            correoNotificacion = correoNotificacion.SelectCorreosById(id);

            return View(correoNotificacion);
        }

        // POST: CorreoNotificaciones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Ytbl_CorreoNotificacionesModels UpdateNotificacion = new Ytbl_CorreoNotificacionesModels();

                UpdateNotificacion.Id = id;
                UpdateNotificacion.Name = collection[2].ToString();
                UpdateNotificacion.Correo = collection[3].ToString();
                UpdateNotificacion.IsValid = collection[4].ToString();

                if (UpdateNotificacion.UpdateCorreo(UpdateNotificacion))
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
