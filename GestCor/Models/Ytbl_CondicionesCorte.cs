using msgprepagosatelital.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class Ytbl_CondicionesCorte
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del banco a excluir")]
        public string Provider { get; set; }
        [Display(Name = "Nombre de ciudad a excluir")]
        public string Ciudad { get; set; }
        [Display(Name = "Modo de pago a excluir")]
        public string PaymentMode { get; set; }
        [Display(Name = "Negocio a excluir")]
        public string Business { get; set; }
        [Display(Name = "Empresa facturadora a excluir")]
        public string Company { get; set; }
        [Required]
        [Display(Name = "Id del corte a excluir")]
        public Int64 Id_Corte { get; set; }
        [Required]
        public string Usuario { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime? Fecha { get; set; }
        [Required]
        [Display(Name = "Es válido")]
        public string IsValid { get; set; }

        Connection conn;

        public void SaveCondicionesCorte(Ytbl_CondicionesCorte CondicionesCorte)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTCONNOCORT";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Provider = new OleDbParameter("PV_PROVIDER", OleDbType.VarChar);
                Provider.Direction = ParameterDirection.Input;
                Provider.Value = CondicionesCorte.Provider;
                cmd.Parameters.Add(Provider);

                OleDbParameter Ciudad = new OleDbParameter("PV_CITY", OleDbType.VarChar);
                Ciudad.Direction = ParameterDirection.Input;
                Ciudad.Value = CondicionesCorte.Ciudad;
                cmd.Parameters.Add(Ciudad);

                OleDbParameter PaymentMode = new OleDbParameter("PV_PAYMENTMODE", OleDbType.VarChar);
                PaymentMode.Direction = ParameterDirection.Input;
                PaymentMode.Value = CondicionesCorte.PaymentMode;
                cmd.Parameters.Add(PaymentMode);

                OleDbParameter Business = new OleDbParameter("PV_BUSINESS", OleDbType.VarChar);
                Business.Direction = ParameterDirection.Input;
                Business.Value = CondicionesCorte.Business;
                cmd.Parameters.Add(Business);

                OleDbParameter Company = new OleDbParameter("PV_COMPANY", OleDbType.VarChar);
                Company.Direction = ParameterDirection.Input;
                Company.Value = CondicionesCorte.Company;
                cmd.Parameters.Add(Company);

                OleDbParameter Id_Corte = new OleDbParameter("PN_ID_CORTE", OleDbType.Integer);
                Id_Corte.Direction = ParameterDirection.Input;
                Id_Corte.Value = CondicionesCorte.Id_Corte;
                cmd.Parameters.Add(Id_Corte);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = CondicionesCorte.IsValid;
                cmd.Parameters.Add(IsValid);

                OleDbParameter Usuario = new OleDbParameter("PV_APPUSER", OleDbType.VarChar);
                Usuario.Direction = ParameterDirection.Input;
                Usuario.Value = CondicionesCorte.Usuario;
                cmd.Parameters.Add(Usuario);

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

        public List<Ytbl_CondicionesCorte> SelectCondicionesCorte()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_CONDICIONESNOCORTE";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            List<Ytbl_CondicionesCorte> CondicionesCorteList = new List<Ytbl_CondicionesCorte>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Ytbl_CondicionesCorte CondicionesCorte = new Ytbl_CondicionesCorte();
                        RecordCount++;

                        CondicionesCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());

                        try { 
                            CondicionesCorte.Provider = myReader.GetString(1).ToString();
                        }
                        catch (Exception ex) { 
                            CondicionesCorte.Provider = null;
                        }
                        try
                        {
                            CondicionesCorte.Ciudad = myReader.GetString(2).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Ciudad = null;
                        }
                        try
                        {
                            CondicionesCorte.PaymentMode = myReader.GetString(3).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.PaymentMode = null;
                        }
                        try
                        {
                            CondicionesCorte.Business = myReader.GetDateTime(4).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Business = null;
                        }
                        try
                        {
                            CondicionesCorte.Company = myReader.GetDateTime(5).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Company = null;
                        }              

                        CondicionesCorte.Id_Corte = Int32.Parse(myReader.GetDecimal(6).ToString());
                        try
                        {
                            CondicionesCorte.Fecha = DateTime.Parse(myReader.GetString(7).ToString());
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Fecha = null;
                        }
                        
                        CondicionesCorte.IsValid = myReader.GetString(8).ToString();
                        CondicionesCorte.Usuario = myReader.GetString(9).ToString();
                        
                        CondicionesCorteList.Add(CondicionesCorte);
                    }
                }

                return CondicionesCorteList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return CondicionesCorteList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public Ytbl_CondicionesCorte SelectCondicionesCorteById(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_CONDICIONESNOCORTE where id =" + id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            Ytbl_CondicionesCorte CondicionCorte = new Ytbl_CondicionesCorte();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                        RecordCount++;

                        CondicionCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        CondicionCorte.Provider = myReader.GetString(1).ToString();
                        CondicionCorte.Ciudad = myReader.GetString(2).ToString();
                        CondicionCorte.PaymentMode = myReader.GetString(3).ToString();
                        CondicionCorte.Business = myReader.GetString(3).ToString();
                        CondicionCorte.Company = myReader.GetString(3).ToString();
                        CondicionCorte.Id = Int32.Parse(myReader.GetString(3).ToString());
                        CondicionCorte.Usuario = myReader.GetString(3).ToString();
                        CondicionCorte.Fecha = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        CondicionCorte.IsValid = myReader.GetString(6).ToString();

                    }
                }

                return CondicionCorte;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return CondicionCorte;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public void UpdateCorreo(Ytbl_CondicionesCorte model)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_UPDATECONNOCORT";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("PN_ID_CORTE", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = model.Id;
                cmd.Parameters.Add(Id);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = model.IsValid;
                cmd.Parameters.Add(IsValid);

                cmd.ExecuteNonQuery();

                objConn.Close();
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en update: " + ex.ToString());
                objConn.Close();
            }
            finally
            {
                objConn.Close();
            }
        }
    }
}