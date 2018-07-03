using GestCor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace GestCor.Controllers
{
    public class CondicionesCorteController : Controller
    {
        // GET: CondicionesCorte
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "CondicionesCorte-Leer")]
        public ActionResult Index()
        {
            Ytbl_CondicionesCorte condicion = new Ytbl_CondicionesCorte();

            List<Ytbl_CondicionesCorte> CondicionList = condicion.SelectCondicionesCorte();

            foreach (var item in CondicionList)
            {
                item.Provider = condicion.getNameProperty(item.Provider, "BANCO");
                item.Ciudad = condicion.getNameProperty(item.Ciudad, "CIUDAD");
                item.Business = condicion.getNameProperty(item.Business, "NEGOCIO");
                item.PaymentMode = condicion.getNameProperty(item.PaymentMode, "PAGO");
                item.Company = condicion.getNameProperty(item.Company, "EMPRESA");
            }

            return View(CondicionList);
        }

        // GET: CondicionesCorte/Create
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "CondicionesCorte-Crear")]
        public ActionResult Create()
        {
            Ytbl_CondicionesCorte condicion = new Ytbl_CondicionesCorte();

            List<SelectListItem> bancos = condicion.getProperty(0,"BANCO");
            List<SelectListItem> ciudades = condicion.getProperty(0, "CIUDAD");
            List<SelectListItem> negocios = condicion.getProperty(0, "NEGOCIO");
            List<SelectListItem> pagos = condicion.getProperty(0, "PAGO");
            List<SelectListItem> empresas = condicion.getProperty(0, "EMPRESA");
            List<SelectListItem> cortes = condicion.getProperty(0, "CORTE");

            ViewData["bancos"] = bancos;
            ViewData["ciudades"] = ciudades;
            ViewData["negocios"] = negocios;
            ViewData["pagos"] = pagos;
            ViewData["empresas"] = empresas;
            ViewData["cortes"] = cortes;

            return View();
        }

        // POST: CondicionesCorte/Create
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "CondicionesCorte-Crear")]
        public ActionResult Create(Ytbl_CondicionesCorte model, string bancos, string ciudades, string negocios, string pagos, string empresas, string cortes)
        {
            string valx = "b=" + bancos + ",n="+ negocios + ",c="+ ciudades+",p="+ pagos +",e="+ empresas;

            Regex Val = new Regex(@"=[0-9a-zA-Z]");

            if (!ModelState.IsValid || !Val.IsMatch(valx))
            {
                
                Ytbl_CondicionesCorte corte = new Ytbl_CondicionesCorte();

                List<SelectListItem> bancosR = corte.getProperty(0, "BANCO");
                ViewData["bancos"] = bancosR;
                List<SelectListItem> ciudadesR = corte.getProperty(0, "CIUDAD");
                ViewData["ciudades"] = ciudadesR;
                List<SelectListItem> negociosR = corte.getProperty(0, "NEGOCIO");
                ViewData["negocios"] = negociosR;
                List<SelectListItem> pagosR = corte.getProperty(0, "PAGO");
                ViewData["pagos"] = pagosR;
                List<SelectListItem> empresasR = corte.getProperty(0, "EMPRESA");
                ViewData["empresas"] = empresasR;
                List<SelectListItem> cortesR = corte.getProperty(0, "CORTE");
                ViewData["cortes"] = cortesR;
                

                return View(model);
            }
            
            Ytbl_CondicionesCorte condicion = new Ytbl_CondicionesCorte();
            HttpContext ctx = System.Web.HttpContext.Current;
            model.Usuario = ctx.Session["usuario"].ToString();

            model.Provider = condicion.getNameProperty(bancos, "BANCO2");
            model.Business = condicion.getNameProperty(negocios, "NEGOCIO2");
            model.PaymentMode = condicion.getNameProperty(pagos, "PAGO2");
            model.Company = condicion.getNameProperty(empresas, "EMPRESA2");
            model.Ciudad = ciudades;
            model.Id_Corte = int.Parse(cortes);

            model.Fecha = DateTime.Now;
            
            try
            {

                if (model.SaveCondicionesCorte(model))
                {
                    TempData["AlertMessage"] = "Condicion guardada exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al crear la condicion";
                    return View("Error");
                }

            }
            catch
            {
                return View("Error");
            }

        }

        // GET: CorreoNotificaciones/Edit/5
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "CondicionesCorte-Editar")]
        public ActionResult Edit(int id)
        {
            Ytbl_CondicionesCorte Condicion = new Ytbl_CondicionesCorte();

            Condicion = Condicion.SelectCondicionesCorteById(id);

            return View(Condicion);
        }

        // POST: CorreoNotificaciones/Edit/5
        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "CondicionesCorte-Editar")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Ytbl_CondicionesCorte UpdateNotificacion = new Ytbl_CondicionesCorte();

                UpdateNotificacion.Id = id;
                UpdateNotificacion.IsValid = collection[2].ToString();

                if (UpdateNotificacion.UpdateCorreo(UpdateNotificacion))
                {
                    TempData["AlertMessage"] = "Condicion editada exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al editar la condicion";
                    return View("Error");
                }


            }
            catch
            {
                return View();
            }
        }
    }
}
