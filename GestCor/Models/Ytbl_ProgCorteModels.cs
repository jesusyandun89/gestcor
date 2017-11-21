using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class Ytbl_ProgCorteModels
    {
        List<ProgCorte> progCorte = new List<ProgCorte>();

        List<DetalleProgCorte> detalleCorte = new List<DetalleProgCorte>();

        public string SaveYtbl_ProgCorte(ProgCorte ProgCorteModels)
        {
            return "Exito";
        }

        public string UpdateYtbl_ProgCorte(ProgCorte ProgCorteModels)
        {
            return "Exito";
        }
    }

    public class ProgCorte
    {
        public string Document_Name { get; set; }
        public string Customer_Number_Upload { get; set; }
        public string Nick_User { get; set; }
        public DateTime Date_Programed { get; set; }
        public DateTime Date_Upload { get; set; }
        public string IsValid { get; set; }
    }
}