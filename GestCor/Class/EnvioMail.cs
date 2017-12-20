using GestCor.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;


namespace GestCor.Class
{
    public class EnvioMail
    {
        public void EnviarCorreo(string destino, int idCorte)
        {
            try
            {
                string body = string.Empty, path = HttpRuntime.AppDomainAppPath;
                //path = path.Substring(0, path.LastIndexOf("\\")) + "\\" + "correo.html";

                StreamReader reader = new StreamReader(path+ "\\Content\\Complement\\correo.html");

                body = reader.ReadToEnd();
                body = body.Replace(":COLOR", "#62A0FF");
                body = body.Replace(":ENTIDAD", "GESTCOR - PROGRAMACION DE CORTE");
                body = body.Replace(":ACCION", "Notificaciones del sistema GESTCOR");
                body = body.Replace(":COMENTARIO", "Existe una nueva programación de corte.");
                body = body.Replace(":OBSERVACION", "Por favor revisar la configuración realizada del nuevo corte con ID: "+ idCorte+" y aprobarla si esta es correcta.");

                MailMessage cMail = new MailMessage();
                cMail.From = new MailAddress("GESTCOR <" + "notificacionhelpdesksistemas@tvcable.com.ec" + ">");

                cMail.To.Add(destino);



                cMail.Subject = "Programación de cortes: GESTCOR";
                cMail.Body = body;
                cMail.IsBodyHtml = true;
                cMail.Priority = MailPriority.Normal;

                /* PARA ADJUNTAR
                Attachment oFile = new Attachment(sFileName);
                oFile.Name = sName;
                cMail.Attachments.Add(oFile);
                */

                string ServerMail = ConfigurationManager.AppSettings.Get("ServerMail");

                SmtpClient oSmtp = new SmtpClient();

                oSmtp.Host = ServerMail;
                oSmtp.Port = System.Convert.ToInt32(25);            
                oSmtp.UseDefaultCredentials = false;
                oSmtp.Send(cMail);
                //MessageBox.Show("Correo enviado");
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error al enviar mail "+ex);
            }
        }
    }
}