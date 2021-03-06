﻿using GestCor.Clases;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;

namespace GestCor.Models
{
    public class Ytbl_DetalleProgCorteModels
    {
        public int counter { get; set; }
        [Required]
        public Int64 id_ProgCorte { get; set; }
        [Required]
        public Int64? CpartyId { get; set; }
        [Required]
        public Int64? CpartyAccountId { get; set; }
        [Required]
        public Int64? CitemId { get; set; }
        [Required]
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
        [Required]
        public string Status { get; set; }

        Connection conn;

        public void SaveYtbl_DetalleProgCorte(Ytbl_DetalleProgCorteModels DetalleCorte)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTDETALLEPROGCORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter IdProgCorte = new OleDbParameter("PN_ID_PROGCORTE", OleDbType.Integer);
                IdProgCorte.Direction = ParameterDirection.Input;
                IdProgCorte.Value = DetalleCorte.id_ProgCorte;
                cmd.Parameters.Add(IdProgCorte);

                OleDbParameter Cparty = new OleDbParameter("PN_CPARTY_ID", OleDbType.BigInt);
                Cparty.Direction = ParameterDirection.Input;
                Cparty.Value = DetalleCorte.CpartyId;
                cmd.Parameters.Add(Cparty);

                OleDbParameter Account = new OleDbParameter("PN_CPARTYACCOUNT_ID", OleDbType.BigInt);
                Account.Direction = ParameterDirection.Input;
                Account.Value = DetalleCorte.CpartyAccountId;
                cmd.Parameters.Add(Account);

                OleDbParameter Citem = new OleDbParameter("PN_CITEM_ID", OleDbType.BigInt);
                Citem.Direction = ParameterDirection.Input;
                Citem.Value = DetalleCorte.CitemId;
                cmd.Parameters.Add(Citem);

                OleDbParameter Pago = new OleDbParameter("PV_FORMAPAGO", OleDbType.VarChar);
                Pago.Direction = ParameterDirection.Input;
                Pago.Value = DetalleCorte.FormaPago;
                cmd.Parameters.Add(Pago);

                OleDbParameter Ciudad = new OleDbParameter("PV_CIUDAD", OleDbType.VarChar);
                Ciudad.Direction = ParameterDirection.Input;
                Ciudad.Value = DetalleCorte.Ciudad;
                cmd.Parameters.Add(Ciudad);

                OleDbParameter Banco = new OleDbParameter("PN_BANCO_ID", OleDbType.VarChar);
                Banco.Direction = ParameterDirection.Input;
                Banco.Value = DetalleCorte.BancoId;
                cmd.Parameters.Add(Banco);

                OleDbParameter Negocio = new OleDbParameter("PV_BUSINESS", OleDbType.VarChar);
                Negocio.Direction = ParameterDirection.Input;
                Negocio.Value = DetalleCorte.TipoNegocio;
                cmd.Parameters.Add(Negocio);

                OleDbParameter Empresa = new OleDbParameter("PV_COMPANY", OleDbType.VarChar);
                Empresa.Direction = ParameterDirection.Input;
                Empresa.Value = DetalleCorte.EmpresaFacturadora;
                cmd.Parameters.Add(Empresa);

                OleDbParameter Status = new OleDbParameter("PV_STATUS", OleDbType.VarChar);
                Status.Direction = ParameterDirection.Input;
                Status.Value = DetalleCorte.Status;
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