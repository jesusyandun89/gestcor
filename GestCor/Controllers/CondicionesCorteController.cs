using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class CondicionesCorteController : Controller
    {
        // GET: CondicionesCorte
        public ActionResult Index()
        {
            Ytbl_CondicionesCorte Condicion = new Ytbl_CondicionesCorte();

            return View(Condicion.SelectCondicionesCorte());
        }

        // GET: CondicionesCorte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CondicionesCorte/Create
        [HttpPost]
        public ActionResult Create(Ytbl_CondicionesCorte model)
        {
            model.Usuario = "jyandun";
            model.Fecha = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {

                if(model.SaveCondicionesCorte(model))
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
            Ytbl_CondicionesCorte Condicion = new Ytbl_CondicionesCorte();

            Condicion = Condicion.SelectCondicionesCorteById(id);

            return View(Condicion);
        }

        // POST: CorreoNotificaciones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Ytbl_CondicionesCorte UpdateNotificacion = new Ytbl_CondicionesCorte();

                UpdateNotificacion.Id = id;
                UpdateNotificacion.IsValid = collection[2].ToString();

                if (UpdateNotificacion.UpdateCorreo(UpdateNotificacion))
                    return RedirectToAction("Index");
                else
                    return View("Error");


            }
            catch
            {
                return View();
            }
        }
    }
}
