using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class Ytbl_DetalleProgCorteModels
    {
        List<DetalleProgCorte> DetalleCorte = new List<DetalleProgCorte>();

        public string SaveYtbl_DetalleProgCorte(DetalleProgCorte DetalleCorte)
        {
            return "Exito";
        }

        public string UpdateYtbl_DetalleProgCorte(DetalleProgCorte DetalleCorte)
        {
            return "Exito";
        }

    }

    public class DetalleProgCorte
    {
        public int counter { get; set; }
        public Int64 id_ProgCorte { get; set; }
        public Int64? CpartyId { get; set; }
        public Int64? CpartyAccountId { get; set; }
        public Int64? CitemId { get; set; }
        public int? Ciudad { get; set; }
        public string BancoId { get; set; }
        public string FormaPago { get; set; }
        public string EmpresaFacturadora { get; set; }
        public string TipoNegocio { get; set; }
        public string FieldV1 { get; set; }
        public string FieldV2 { get; set; }
        public int? FieldN1 { get; set; }
        public int? FieldN2 { get; set; }
        public DateTime? FieldD1 { get; set; }
        public string Status { get; set; }
    }
}