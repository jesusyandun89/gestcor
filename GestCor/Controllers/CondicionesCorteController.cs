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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Ytbl_CondicionesCorte Condicion = new Ytbl_CondicionesCorte();

                Condicion.Provider = collection[1].ToString();
                Condicion.Ciudad = collection[2].ToString();
                Condicion.PaymentMode = collection[3].ToString();
                Condicion.Business = collection[4].ToString();
                Condicion.Company = collection[5].ToString();
                Condicion.Id_Corte = Int64.Parse(collection[6].ToString());
                Condicion.IsValid = collection[7].ToString();
                Condicion.Usuario = "jyandun";
                

                Condicion.SaveCondicionesCorte(Condicion);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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

                UpdateNotificacion.UpdateCorreo(UpdateNotificacion);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
