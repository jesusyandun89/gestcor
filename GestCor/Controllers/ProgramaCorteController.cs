using GestCor.Models;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestCor.Controllers
{
    public class ProgramaCorteController : Controller
    {
        // GET: ProgramaCorte
        public ActionResult Index()
        {
            Ytbl_ProgCorteModels progCorte = new Ytbl_ProgCorteModels();

            List<Ytbl_ProgCorteModels> listProgCorte = new List<Ytbl_ProgCorteModels>();

            listProgCorte = progCorte.SelectYtbl_ProgCorte();

            return View(listProgCorte);
        }



        private bool VerifyFile(HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                if (upload.FileName.EndsWith(".csv"))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        private List<Ytbl_DetalleProgCorteModels> UploadFile(HttpPostedFileBase upload)
        {
            List<Ytbl_DetalleProgCorteModels> DetalleCorteLista = new List<Ytbl_DetalleProgCorteModels>();
            int i = 0;

            string fileName = upload.FileName;
            
            string filePath4 = Path.GetTempPath();

            string filePath = Path.Combine(filePath4, fileName);

            upload.SaveAs(filePath);

            using (var reader = new StreamReader(filePath))
            {                
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    Ytbl_DetalleProgCorteModels DetalleProgCorte = new Ytbl_DetalleProgCorteModels();

                    DetalleProgCorte.counter = i++;

                    if (values[0] != null)
                        DetalleProgCorte.CpartyId = Int64.Parse(values[0]);
                    else
                        DetalleProgCorte.CpartyId = null;

                    if (values[1] != null)
                        DetalleProgCorte.CpartyAccountId = Int64.Parse(values[1]);
                    else
                        DetalleProgCorte.CpartyAccountId = null;

                    if (values[2] != null)
                        DetalleProgCorte.CitemId = Int64.Parse(values[2]);
                    else
                        DetalleProgCorte.CitemId = null;

                    if (values[3] != null)
                        DetalleProgCorte.Ciudad = int.Parse(values[3]);
                    else
                        DetalleProgCorte.Ciudad = null;

                    if (values[4] != null)
                        DetalleProgCorte.BancoId = values[4];
                    else
                        DetalleProgCorte.BancoId = null;

                    if (values[5] != null)
                        DetalleProgCorte.FormaPago = values[5];
                    else
                        DetalleProgCorte.FormaPago = null;

                    if (values[6] != null)
                        DetalleProgCorte.EmpresaFacturadora = values[6];
                    else
                        DetalleProgCorte.EmpresaFacturadora = null;

                    if (values[7] != null)
                        DetalleProgCorte.TipoNegocio = values[7];
                    else
                        DetalleProgCorte.TipoNegocio = null;

                    try
                    {
                        if (values[8] != null)
                            DetalleProgCorte.FieldV1 = values[8];
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldV1 = null;
                    }

                    try
                    {
                        if (values[9] != null)
                            DetalleProgCorte.FieldV2 = values[9];
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldV2 = null;
                    }    

                    try
                    {
                        if (values[10] != null)
                            DetalleProgCorte.FieldN1 = int.Parse(values[10]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldN1 = null;
                    }

                    try
                    {
                        if (values[11] != null)
                            DetalleProgCorte.FieldN2 = int.Parse(values[11]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldN2 = null;
                    }

                    try
                    {
                        if (values[12] != null)
                            DetalleProgCorte.FieldD1 = DateTime.Parse(values[12]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldD1 = null;
                    }

                    DetalleProgCorte.Status = "Q";

                    DetalleCorteLista.Add(DetalleProgCorte);
                }
            }
            Ytbl_ProgCorteModels CreateNew = new Ytbl_ProgCorteModels();

            CreateNew.Document_Name = fileName;
            CreateNew.Customer_Number_Upload = i.ToString();
            CreateNew.Nick_User = "jyandun";

            List<Ytbl_ProgCorteModels> ListProgCorte = new List<Ytbl_ProgCorteModels>();

            ListProgCorte.Add(CreateNew);

            Ytbl_ProgCorteModels.ListProgCorte = null;
            Ytbl_ProgCorteModels.ListProgCorte = ListProgCorte;

            Ytbl_ProgCorteModels.DetalleCorte = null;
            Ytbl_ProgCorteModels.DetalleCorte = DetalleCorteLista;

            return DetalleCorteLista;
        }

        private List<Estadisticas> getBancos(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();
            var datos = from p in DetalleCorte
                         group p.BancoId by p.BancoId into g
                         select new { Nombre = g.Key, Cantidad = g.Count() };

            foreach (var item in datos)
            {
                Estadisticas estadistica = new Estadisticas();

                try
                {
                    estadistica.cantidad = int.Parse(item.Cantidad.ToString());
                    if (item.Nombre.ToString() != "")
                        estadistica.nombre = item.Nombre.ToString();
                    else
                        estadistica.nombre = "Ninguno";
                }
                catch(IndexOutOfRangeException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getCiudades(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();
            var datos = from p in DetalleCorte
                        group p.Ciudad by p.Ciudad into g
                        select new { Nombre = g.Key, Cantidad = g.Count() };

            foreach (var item in datos)
            {
                Estadisticas estadistica = new Estadisticas();

                try
                {
                    estadistica.cantidad = int.Parse(item.Cantidad.ToString());
                    if (item.Nombre.ToString() != "")
                        estadistica.nombre = item.Nombre.ToString();
                    else
                        estadistica.nombre = "Ninguno";
                }
                catch (IndexOutOfRangeException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getTipoNegocios(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();
            var datos = from p in DetalleCorte
                        group p.TipoNegocio by p.TipoNegocio into g
                        select new { Nombre = g.Key, Cantidad = g.Count() };

            foreach (var item in datos)
            {
                Estadisticas estadistica = new Estadisticas();

                try
                {
                    estadistica.cantidad = int.Parse(item.Cantidad.ToString());
                    if (item.Nombre.ToString() != "")
                        estadistica.nombre = item.Nombre.ToString();
                    else
                        estadistica.nombre = "Ninguno";

                }
                catch (IndexOutOfRangeException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getEmpresaFacturadoras(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();
            var datos = from p in DetalleCorte
                        group p.EmpresaFacturadora by p.EmpresaFacturadora into g
                        select new { Nombre = g.Key, Cantidad = g.Count() };

            foreach (var item in datos)
            {
                Estadisticas estadistica = new Estadisticas();

                try
                {
                    estadistica.cantidad = int.Parse(item.Cantidad.ToString());
                    if (item.Nombre.ToString() != "")
                        estadistica.nombre = item.Nombre.ToString();
                    else
                        estadistica.nombre = "Ninguno";
                }
                catch (IndexOutOfRangeException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getCantidadCuentas(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();



            var cuentasTotal = (from item in DetalleCorte select item.CpartyAccountId).Distinct().Count();

            Estadisticas estadistica = new Estadisticas();
            estadistica.cantidad = int.Parse(cuentasTotal.ToString());
            estadistica.nombre = "Cuentas";

            estadisticasList.Add(estadistica);

            return estadisticasList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (VerifyFile(upload))
                {
                    List<Ytbl_DetalleProgCorteModels> DetalleCorte = UploadFile(upload);

                    ViewData["bancos"] = getBancos(DetalleCorte);

                    ViewData["ciudad"]  = getCiudades(DetalleCorte);

                    ViewData["negocio"] = getTipoNegocios(DetalleCorte);

                    ViewData["empresaFacturadora"] = getEmpresaFacturadoras(DetalleCorte);

                    ViewData["cuentas"] = getCantidadCuentas(DetalleCorte);

                    return View();
                }
                else
                {
                    ModelState.AddModelError("Archivo", "El archivo no tiene la extensión correcta");
                }
            }
            return View();
        }

        public ActionResult Create()
        {
            Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();

            foreach (var item in Ytbl_ProgCorteModels.ListProgCorte)
            {
                ProgCorte.Document_Name = item.Document_Name;
                ProgCorte.Customer_Number_Upload = item.Customer_Number_Upload;
                ProgCorte.Nick_User = item.Nick_User;
                ProgCorte.Date_Programed = DateTime.Now;
                ProgCorte.Date_Upload = DateTime.Now;
                ProgCorte.IsValid = "Y";
            }

            return View(ProgCorte);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ytbl_ProgCorteModels model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            foreach (var item in Ytbl_ProgCorteModels.ListProgCorte)
            {
                model.Document_Name = item.Document_Name;
                model.Customer_Number_Upload = item.Customer_Number_Upload;
                model.Nick_User = item.Nick_User;
                model.Date_Upload = DateTime.Now;
            }

            Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();


            ProgCorte.ExecuteSave(model);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Ytbl_ProgCorteModels progCorte = new Ytbl_ProgCorteModels();

            return View(progCorte.SelectYtbl_ProgCorte(id));
        }
    }
}