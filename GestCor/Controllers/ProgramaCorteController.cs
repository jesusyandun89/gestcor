using GestCor.Models;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestCor.Clases;


namespace GestCor.Controllers
{
    public class ProgramaCorteController : Controller
    {

        // GET: ProgramaCorte
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Leer")]
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

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Crear")]
        public ActionResult UploadFile()
        {
            return View();
        }

        private List<Ytbl_DetalleProgCorteModels> UploadFile(HttpPostedFileBase upload)
        {
            List<Ytbl_DetalleProgCorteModels> DetalleCorteLista = new List<Ytbl_DetalleProgCorteModels>();
            int i = 0;
            DetalleCorteLista.Clear();
            
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
                        ex.ToString();
                    }

                    try
                    {
                        if (values[9] != null)
                            DetalleProgCorte.FieldV2 = values[9];
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldV2 = null;
                        ex.ToString();
                    }    

                    try
                    {
                        if (values[10] != null)
                            DetalleProgCorte.FieldN1 = int.Parse(values[10]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldN1 = null;
                        ex.ToString();
                    }

                    try
                    {
                        if (values[11] != null)
                            DetalleProgCorte.FieldN2 = int.Parse(values[11]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldN2 = null;
                        ex.ToString();
                    }

                    try
                    {
                        if (values[12] != null)
                            DetalleProgCorte.FieldD1 = DateTime.Parse(values[12]);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DetalleProgCorte.FieldD1 = null;
                        ex.ToString();
                    }

                    DetalleProgCorte.Status = "P";

                    DetalleCorteLista.Add(DetalleProgCorte);
                }
            }
            Ytbl_ProgCorteModels CreateNew = new Ytbl_ProgCorteModels();
            HttpContext ctx = System.Web.HttpContext.Current;
            string User = ctx.Session["usuario"].ToString();

            CreateNew.Document_Name = fileName;
            CreateNew.Customer_Number_Upload = i.ToString();
            CreateNew.Nick_User = User;
            CreateNew.IsValid = "N";

            List<Ytbl_ProgCorteModels> ListProgCorte = new List<Ytbl_ProgCorteModels>();

            ListProgCorte.Add(CreateNew);

            Ytbl_ProgCorteModels.ListProgCorte  = null;
            Ytbl_ProgCorteModels.ListProgCorte = ListProgCorte;

            Ytbl_ProgCorteModels.DetalleCorte = null;
            Ytbl_ProgCorteModels.DetalleCorte = DetalleCorteLista;

            return DetalleCorteLista;
        }

        private List<Estadisticas> getBancos(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();

            var datos =


                from com1 in (
                from com2 in DetalleCorte
                    //where com2.cantidad.ToString().Contains("103")
                group com2 by new { com2.CpartyAccountId, com2.BancoId } into b
                select b
                )

                group com1 by com1.Key.BancoId into z

                select new
                {
                    Cantidad = z.Count(),
                    Nombre = z.Key
                }
                ;

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
                    ex.ToString();
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                    ex.ToString();
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getCiudades(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();

            Ytbl_CondicionesCorte condicion = new Ytbl_CondicionesCorte();

            var datos =


                from com1 in (
                from com2 in DetalleCorte
                    //where com2.cantidad.ToString().Contains("103")
                group com2 by new { com2.CpartyAccountId, com2.Ciudad } into b
                select b
                )

                group com1 by com1.Key.Ciudad into z

                select new
                {
                    Cantidad = z.Count(),
                    Nombre = z.Key
                }
                ;

            foreach (var item in datos)
            {
                Estadisticas estadistica = new Estadisticas();

                try
                {
                    estadistica.cantidad = int.Parse(item.Cantidad.ToString());
                    if (item.Nombre.ToString() != "")
                        estadistica.nombre = condicion.getNameProperty(item.Nombre.ToString(), "CIUDAD");
                    else
                        estadistica.nombre = "Ninguno";
                }
                catch (IndexOutOfRangeException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                    ex.ToString();
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                    ex.ToString();
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getTipoNegocios(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();

            var datos =


                from com1 in (
                from com2 in DetalleCorte
                    //where com2.cantidad.ToString().Contains("103")
                group com2 by new { com2.CpartyAccountId, com2.TipoNegocio } into b
                select b
                )

                group com1 by com1.Key.TipoNegocio into z

                select new
                {
                    Cantidad = z.Count(),
                    Nombre = z.Key
                }
                ;

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
                    ex.ToString();
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                    ex.ToString();
                }


                estadisticasList.Add(estadistica);
            }

            return estadisticasList;
        }

        private List<Estadisticas> getEmpresaFacturadoras(List<Ytbl_DetalleProgCorteModels> DetalleCorte)
        {
            List<Estadisticas> estadisticasList = new List<Estadisticas>();

            var datos =


                from com1 in (
                from com2 in DetalleCorte
                    //where com2.cantidad.ToString().Contains("103")
                group com2 by new { com2.CpartyAccountId, com2.EmpresaFacturadora } into b
                select b
                )

                group com1 by com1.Key.EmpresaFacturadora into z

                select new
                {
                    Cantidad = z.Count(),
                    Nombre = z.Key
                }
                ;

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
                    ex.ToString();
                }
                catch (FormatException ex)
                {
                    estadistica.cantidad = 0;
                    estadistica.nombre = "Ninguno";
                    ex.ToString();
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
        [Authorize]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Crear")]
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

                    ViewBag.view = true;

                    return View();
                }
                else
                {
                    ViewBag.view = false;
                    ModelState.AddModelError("", "Error en la carga del archivo..");
                    return View();
                }
            }
            ViewBag.view = false;
            return View("Error");
        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Crear")]
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
                ProgCorte.IsValid = "N";
            }
            
            return View(ProgCorte);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Crear")]
        public ActionResult Create(Ytbl_ProgCorteModels model)
        {

            foreach (var item in Ytbl_ProgCorteModels.ListProgCorte)
            {
                model.Document_Name = item.Document_Name;
                model.Document_Name = item.Document_Name;
                model.Customer_Number_Upload = item.Customer_Number_Upload;
                model.Nick_User = item.Nick_User;
                model.Date_Programed = DateTime.Now;
                model.Date_Upload = DateTime.Now;
                model.IsValid = "N";
            }


            if (model.ExecuteSave(model))
            {
                TempData["AlertMessage"] = "Corte creado con exitosamente";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Error al crear el corte";
                return View("Error");
            }


        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Editar")]
        public ActionResult Edit(int id)
        {
            Ytbl_ProgCorteModels progCorte = new Ytbl_ProgCorteModels();

            List<Ytbl_ProgCorteModels> ListProgCorte = new List<Ytbl_ProgCorteModels>();

            progCorte = progCorte.SelectYtbl_ProgCorteId(id);

            ListProgCorte.Add(progCorte);

            Ytbl_ProgCorteModels.ListProgCorte = ListProgCorte;
       
            return View(progCorte);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Editar")]
        public ActionResult Edit(Ytbl_ProgCorteModels model, string edit)
        {
            if (edit == "true")
            {
                foreach (var item in Ytbl_ProgCorteModels.ListProgCorte)
                {
                    bool flag = model.EvaluaExcepciones((int)model.Id, model.IsValid);
                    Logs.WriteErrorLog("EvaluaExcepciones: " + flag);

                    model.Document_Name = item.Document_Name;
                    model.Customer_Number_Upload = item.Customer_Number_Upload;
                    model.Nick_User = item.Nick_User;

                    model.Date_Upload = item.Date_Upload;
                    model.Date_Programed = item.Date_Programed;
                }

                Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();

                if (ProgCorte.validateUpdate(model.Id))
                {
                    if (ProgCorte.UpdateYtbl_ProgCorte(model))
                    {

                        TempData["AlertMessage"] = "Corte editado con exitosamente";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Error al editar el corte";
                        return View("Error");
                    }
                }
                else
                {
                    TempData["AlertMessage"] = "El corte solo puede ser editado dentro del mes en curso";
                    return RedirectToAction("Index");
                }
                
            }

            return RedirectToAction("Index");

        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Leer")]
        public ActionResult View(int id)
        {
            Ytbl_ProgCorteModels model = new Ytbl_ProgCorteModels();

            ViewData["bancos"] = model.getStadistic(id, "BANCO");

            ViewData["ciudad"] = model.getStadistic(id, "CIUDAD");

            ViewData["negocio"] = model.getStadistic(id, "BUSINESS");

            ViewData["empresaFacturadora"] = model.getStadistic(id, "COMPANY");

            ViewData["cuentas"] = model.getStadistic(id, "CUENTAS");

            ViewBag.view = true;

            return View();

        }

        [Authorize]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Leer")]
        public ActionResult Result(int id)
        {
            Ytbl_ProgCorteModels corteResult = new Ytbl_ProgCorteModels();

            return View(corteResult.getReport(id, true));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [CustomAuthorizeAttribute(Roles = "ProgramaCorte-Leer")]
        public ActionResult Result(int id, string value1)
        {
            Ytbl_ProgCorteModels corteResult = new Ytbl_ProgCorteModels();

            var gv = new GridView();
            gv.DataSource = corteResult.getReport(id, false);
            gv.DataBind();
            if (gv.PageCount > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
                
        }
    }
}