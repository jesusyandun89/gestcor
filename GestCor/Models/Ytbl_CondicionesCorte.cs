using GestCor.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Display(Name = "Corte a excluir (id - Nombre de documento)")]
        public Int64 Id_Corte { get; set; }

        public string Usuario { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime? Fecha { get; set; }

        [Required]
        [Display(Name = "Es válido")]
        public string IsValid { get; set; }

        public bool SaveCondicionesCorte(Ytbl_CondicionesCorte CondicionesCorte)
        {
            Connection conn = new Connection();
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

                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public List<Ytbl_CondicionesCorte> SelectCondicionesCorte()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_CONDICIONESNOCORTE order by id desc";

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
                            CondicionesCorte.Business = myReader.GetString(4).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Business = null;
                        }
                        try
                        {
                            CondicionesCorte.Company = myReader.GetString(5).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionesCorte.Company = null;
                        }              

                        CondicionesCorte.Id_Corte = Int32.Parse(myReader.GetDecimal(6).ToString());
                        try
                        {
                            CondicionesCorte.Fecha = DateTime.Parse(myReader.GetDateTime(7).ToString());
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
            Connection conn = new Connection();
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

                        try
                        {
                            CondicionCorte.Provider = myReader.GetString(1).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.Provider = null;
                        }
                        try
                        {
                            CondicionCorte.Ciudad = myReader.GetString(2).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.Ciudad = null;
                        }
                        try
                        {
                            CondicionCorte.PaymentMode = myReader.GetString(3).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.PaymentMode = null;
                        }
                        try
                        {
                            CondicionCorte.Business = myReader.GetString(4).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.Business = null;
                        }
                        try
                        {
                            CondicionCorte.Company = myReader.GetString(5).ToString();
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.Company = null;
                        }

                        CondicionCorte.Id_Corte = Int32.Parse(myReader.GetDecimal(6).ToString());
                        try
                        {
                            CondicionCorte.Fecha = DateTime.Parse(myReader.GetDateTime(7).ToString());
                        }
                        catch (Exception ex)
                        {
                            CondicionCorte.Fecha = null;
                        }

                        CondicionCorte.IsValid = myReader.GetString(8).ToString();
                        CondicionCorte.Usuario = myReader.GetString(9).ToString();

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

        public bool UpdateCorreo(Ytbl_CondicionesCorte model)
        {
            Connection conn = new Connection();
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

                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en update: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public List<SelectListItem> getProperty(int id, string property)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "";

            switch (property)
            {
                case "BANCO":
                    commText = "SELECT B.NAME, B.id, decode(B.PAYMENTPROVIDER_ID, "+id+", 'true','false') FROM TAMPAYMENTPROVIDERBRANCHES B order by name asc";
                    break;
                case "CIUDAD":
                    commText = "SELECT t2.name, t2.avalue, decode(t2.avalue, " + id + ", 'true','false') FROM TWFLREPVALUELISTITEMS T2 where t2.valuelistsymbol like '%TVC_CityVsCostCenter%'";
                    break;
                case "NEGOCIO":
                    commText = "SELECT name, id, decode(id, " + id + ", 'true','false') FROM TREPVALUELISTITEMS where valuelist_id=500006 order by name asc";
                    break;
                case "PAGO":
                    commText = "SELECT name, id, decode(id, " + id + ", 'true','false') FROM TREPVALUELISTITEMS WHERE VALUELISTSYMBOL LIKE '%PaymentType%'";
                    break;
                case "EMPRESA":
                    commText = "SELECT NAME, ID, decode(id, " + id + ", 'true','false') FROM TREPVALUELISTITEMS where valuelist_id = 500031 order by name asc";
                    break;
                case "CORTE":
                    commText = "select concat(concat('id: ' , concat(id , ' - ')),DOCUMENT_NAME), id, decode(id, 0, 'true','false') from YTBL_PROGCORTE where ISVALID = 'N' and DATE_UPLOAD >= sysdate - 90 order by id desc";
                    break;
            }

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<SelectListItem> RolesList = new List<SelectListItem>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        SelectListItem rol = new SelectListItem();
                        rol.Value = myReader.GetValue(1).ToString();
                        rol.Text = myReader.GetValue(0).ToString();

                        RolesList.Add(rol);
                    }
                }

                return RolesList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return RolesList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public string getNameProperty(string id, string property)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "";

            switch (property)
            {
                case "BANCO":
                    commText = "SELECT B.NAME FROM TAMPAYMENTPROVIDERBRANCHES B where B.name ='"+id+"'";
                    break;
                case "BANCO2":
                    commText = "SELECT B.NAME FROM TAMPAYMENTPROVIDERBRANCHES B where B.id ='" + id + "'";
                    break;
                case "CIUDAD":
                    commText = "SELECT t2.name FROM TWFLREPVALUELISTITEMS T2 where t2.valuelistsymbol like '%TVC_CityVsCostCenter%' and t2.avalue = '"+id+"'";
                    break;
                case "NEGOCIO":
                    commText = "SELECT name FROM TREPVALUELISTITEMS where valuelist_id=500006 and name ='"+id+"'";
                    break;
                case "NEGOCIO2":
                    commText = "SELECT name FROM TREPVALUELISTITEMS where valuelist_id=500006 and id ='" + id + "'";
                    break;
                case "PAGO":
                    commText = "SELECT name FROM TREPVALUELISTITEMS WHERE VALUELISTSYMBOL LIKE '%PaymentType%' and name ='" + id+"'";
                    break;
                case "PAGO2":
                    commText = "SELECT name FROM TREPVALUELISTITEMS WHERE VALUELISTSYMBOL LIKE '%PaymentType%' and id ='" + id + "'";
                    break;
                case "EMPRESA":
                    commText = "SELECT NAME FROM TREPVALUELISTITEMS where valuelist_id = 500031 and name ='" + id+"'";
                    break;
                case "EMPRESA2":
                    commText = "SELECT NAME FROM TREPVALUELISTITEMS where valuelist_id = 500031 and id ='" + id + "'";
                    break;
            }

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            string propertyName ="";
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        try
                        { propertyName = myReader.GetValue(0).ToString(); }
                        catch (Exception ex) { propertyName = ""; }
                    }
                }

                return propertyName;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return propertyName;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }
    }
}