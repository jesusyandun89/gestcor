using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class Ytbl_CorreoNotificacionesModels
    {
        public string Name { get; set; }

        public string Correo { get; set; }

        public string IsValid { get; set; }

        public string System { get; set; }

        public DateTime Fecha { get; set; }

        public string SaveYtbl_CorreoNotificaciones(Ytbl_CorreoNotificacionesModels CorreoNotificacionesModels)
        {
            return "Exito";
        }
    }
}