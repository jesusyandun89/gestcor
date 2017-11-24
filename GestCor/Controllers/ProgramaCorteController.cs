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
            return View();
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

        private List<DetalleProgCorte> UploadFile(int idProgCorte)
        {
            List<DetalleProgCorte> DetalleCorteLista = new List<DetalleProgCorte>();
            int i = 1;

            using (var reader = new StreamReader(@"C:\Clientes.csv"))
            {                
                string headerLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    DetalleProgCorte DetalleProgCorte = new DetalleProgCorte();

                    DetalleProgCorte.counter = i++;
                    DetalleProgCorte.id_ProgCorte = idProgCorte;

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

            return DetalleCorteLista;
        }

        private List<Estadisticas> getBancos(List<DetalleProgCorte> DetalleCorte)
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

        private List<Estadisticas> getCiudades(List<DetalleProgCorte> DetalleCorte)
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

        private List<Estadisticas> getTipoNegocios(List<DetalleProgCorte> DetalleCorte)
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

        private List<Estadisticas> getEmpresaFacturadoras(List<DetalleProgCorte> DetalleCorte)
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

        private List<Estadisticas> getCantidadCuentas(List<DetalleProgCorte> DetalleCorte)
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
        public ActionResult _UploadPartial(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (VerifyFile(upload))
                {
                    List<DetalleProgCorte> DetalleCorte = UploadFile(1);

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
    }
}