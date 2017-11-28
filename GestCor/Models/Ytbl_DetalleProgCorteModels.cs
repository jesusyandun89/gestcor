using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using msgprepagosatelital.Clases;
using System.Data;

namespace GestCor.Models
{
    public class Ytbl_DetalleProgCorteModels
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

        Connection conn;

        public void SaveYtbl_DetalleProgCorte()
        {
            conn = new Connection();
            OracleConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTDETALLEPROGCORTE";
                objConn.Open();
                OracleCommand cmd = new OracleCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter IdProgCorte = new OracleParameter("PN_ID_PROGCORTE", OracleDbType.Int64);
                IdProgCorte.Direction = ParameterDirection.Input;
                IdProgCorte.Value = this.id_ProgCorte;
                cmd.Parameters.Add(IdProgCorte);

                OracleParameter Cparty = new OracleParameter("PN_CPARTY_ID", OracleDbType.Int64);
                Cparty.Direction = ParameterDirection.Input;
                Cparty.Value = this.CpartyId;
                cmd.Parameters.Add(Cparty);

                OracleParameter Account = new OracleParameter("PN_CPARTYACCOUNT_ID", OracleDbType.Int64);
                Account.Direction = ParameterDirection.Input;
                Account.Value = this.CpartyAccountId;
                cmd.Parameters.Add(Account);

                OracleParameter Citem = new OracleParameter("PN_CITEM_ID", OracleDbType.Int64);
                Citem.Direction = ParameterDirection.Input;
                Citem.Value = this.CitemId;
                cmd.Parameters.Add(Citem);

                OracleParameter Pago = new OracleParameter("PV_FORMAPAGO", OracleDbType.Varchar2);
                Pago.Direction = ParameterDirection.Input;
                Pago.Value = this.FormaPago;
                cmd.Parameters.Add(Pago);

                OracleParameter Ciudad = new OracleParameter("PV_CIUDAD", OracleDbType.Varchar2);
                Ciudad.Direction = ParameterDirection.Input;
                Ciudad.Value = this.Ciudad;
                cmd.Parameters.Add(Ciudad);

                OracleParameter Banco = new OracleParameter("PN_BANCO_ID", OracleDbType.Varchar2);
                Banco.Direction = ParameterDirection.Input;
                Banco.Value = this.BancoId;
                cmd.Parameters.Add(Banco);

                OracleParameter Negocio = new OracleParameter("PV_BUSINESS", OracleDbType.Varchar2);
                Negocio.Direction = ParameterDirection.Input;
                Negocio.Value = this.TipoNegocio;
                cmd.Parameters.Add(Negocio);

                OracleParameter Empresa = new OracleParameter("PV_COMPANY", OracleDbType.Varchar2);
                Empresa.Direction = ParameterDirection.Input;
                Empresa.Value = this.EmpresaFacturadora;
                cmd.Parameters.Add(Empresa);

                OracleParameter Fieldv1 = new OracleParameter("PV_FIELDV1", OracleDbType.Varchar2);
                Fieldv1.Direction = ParameterDirection.Input;
                Fieldv1.Value = this.FieldV1;
                cmd.Parameters.Add(Fieldv1);

                OracleParameter Fieldv2 = new OracleParameter("PV_FIELDV2", OracleDbType.Varchar2);
                Fieldv2.Direction = ParameterDirection.Input;
                Fieldv2.Value = this.FieldV2;
                cmd.Parameters.Add(Fieldv2);

                OracleParameter Fieldn1 = new OracleParameter("PN_FIELDN1", OracleDbType.Int32);
                Fieldn1.Direction = ParameterDirection.Input;
                Fieldn1.Value = this.FieldN1;
                cmd.Parameters.Add(Fieldn1);

                OracleParameter Fieldn2 = new OracleParameter("PN_FIELDN2", OracleDbType.Int32);
                Fieldn2.Direction = ParameterDirection.Input;
                Fieldn2.Value = this.FieldN2;
                cmd.Parameters.Add(Fieldn2);

                OracleParameter DateField = new OracleParameter("PD_FIELDD1", OracleDbType.Date);
                DateField.Direction = ParameterDirection.Input;
                DateField.Value = this.FieldD1;
                cmd.Parameters.Add(DateField);

                OracleParameter Status = new OracleParameter("PV_STATUS", OracleDbType.Varchar2);
                Status.Direction = ParameterDirection.Input;
                Status.Value = this.Status;
                cmd.Parameters.Add(Status);

                cmd.ExecuteNonQuery();
                objConn.Close();
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
            }
            finally
            {
                objConn.Close();
            }
        }

    }
}