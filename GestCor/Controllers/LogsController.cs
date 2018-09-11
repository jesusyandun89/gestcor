using GestCor.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestCor.Controllers
{
    public class LogsController : Controller
    {
        // GET: Logs
        [CustomAuthorizeAttribute(Roles = "Logs-Leer")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Logs-Leer")]
        public ActionResult Create(DateTime fecha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string valCon = ConfigurationManager.AppSettings.Get("DirectoryLogs").ToString();

                    string fullName = @"" + valCon + "/logUserApp-" + fecha.ToString("MM-dd-yy") + ".txt";

                    byte[] fileBytes = GetFile(fullName);

                    if(fileBytes != null)
                        return File(
                        fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "logUserApp-" + fecha.ToString("MM-dd-yy") + ".txt");
                    else
                        return View("Error");
                }
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                Logs.WriteErrorLog("Error en la busqueda del archivo||" + ex.ToString());
                return View("Error");
            }

        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        [HttpPost]
        [Authorize]
        [CustomAuthorizeAttribute(Roles = "Logs-Leer")]
        public ActionResult CreateLogServicio(DateTime fecha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string valCon = ConfigurationManager.AppSettings.Get("DirectoryLogsService").ToString();

                    string fullName = @"" + valCon + "/logUser-" + fecha.ToString("MM-dd-yy") + ".txt";

                    byte[] fileBytes = GetFile(fullName);

                    if (fileBytes != null)
                        return File(
                        fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "logUser-" + fecha.ToString("MM-dd-yy") + ".txt");
                    else
                        return View("Error");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en la busqueda del archivo||" + ex.ToString());
                return View("Error");
            }

        }
    }
}
