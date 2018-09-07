using GestCor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestCor.Controllers
{
    public class DunningMasivoController : Controller
    {
        GenerarDunningMasivoModels GenerarDunningMasivo = new GenerarDunningMasivoModels();
        // GET: DunningMasivo
        //[CustomAuthorizeAttribute(Roles = "DunningMasivo-Leer")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        //[CustomAuthorizeAttribute(Roles = "DunningMasivo-Leer")]
        public ActionResult DescargarInformacion()
        {
            try
            {
                List<GenerarDunningMasivo> GenerarDunningMasivoList = new List<GenerarDunningMasivo>();

                var gv = new GridView();
                gv.DataSource = GenerarDunningMasivoList = GenerarDunningMasivo.getDunningMasivo();
                gv.DataBind();
                if (gv.PageCount > 0)
                {
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=Cuentas.xls");
                    Response.ContentType = "text/CSV";
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
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        //[CustomAuthorizeAttribute(Roles = "DunningMasivo-Crear")]
        public ActionResult Upload(HttpPostedFileBase upload, string uploadFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (VerifyFile(upload))
                    {
                        if(uploadFile == "true")
                        {
                            List<GenerarDunningMasivo> DunningMasivoList = new List<GenerarDunningMasivo>();

                            DunningMasivoList.Clear();

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
                                    GenerarDunningMasivo dunningMasivo = new GenerarDunningMasivo();

                                    if (values[0] != null)
                                        dunningMasivo.ACCOUNT_ID = Int64.Parse(values[0]);

                                    DunningMasivoList.Add(dunningMasivo);
                                }
                            }

                            GenerarDunningMasivo.dunningMasivoList = DunningMasivoList;

                            GenerarDunningMasivo.truncateDunningMasico();

                            GenerarDunningMasivo.saveDunningMasivo();

                            TempData["AlertMessage"] = "Archivo cargado exitosamente";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewBag.view = false;
                        TempData["AlertMessage"] = "Archivo no cumple con el formato establecido";
                        ModelState.AddModelError("", "Error en la carga del archivo..");
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.view = false;
                return View("Error");
            }
            catch(Exception ex)
            {
                return View("Error");
            }
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

        //[CustomAuthorizeAttribute(Roles = "DunningMasivo-Crear")]
        public ActionResult BorrarDatos(string borrar)
        {
            try
            {
                if(borrar == "true")
                {
                    GenerarDunningMasivo.truncateDunningMasico();
                    TempData["AlertMessage"] = "Borrado de datos ejecutado exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["AlertMessage"] = "Error al borrar los datos de la tabla";
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
