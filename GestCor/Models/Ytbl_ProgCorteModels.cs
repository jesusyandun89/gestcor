using msgprepagosatelital.Clases;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace GestCor.Models
{
    public class Ytbl_ProgCorteModels
    {
        public int? Id { get; set; }

        [Display(Name = "Nombre del documento")]
        public string Document_Name { get; set; }
        [Display(Name = "Cantidad de clientes")]
        public string Customer_Number_Upload { get; set; }
        [Display(Name = "Usuario")]
        public string Nick_User { get; set; }
        [Display(Name = "Fecha programada")]
        public DateTime Date_Programed { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime Date_Upload { get; set; }
        [Display(Name = "Es válido")]
        public string IsValid { get; set; }

        Connection conn;

        public List<Ytbl_DetalleProgCorteModels> DetalleCorte { get; set; }

        public void SaveYtbl_ProgCorte()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTPROGCORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Document = new OleDbParameter("PN_DOCUMENT_NAME", OleDbType.VarChar);
                Document.Direction = ParameterDirection.Input;
                Document.Value = this.Document_Name;
                cmd.Parameters.Add(Document);

                OleDbParameter Customers = new OleDbParameter("PN_CUSTOMES_NUMBER_UPLOAD", OleDbType.VarChar);
                Customers.Direction = ParameterDirection.Input;
                Customers.Value = this.Customer_Number_Upload;
                cmd.Parameters.Add(Customers);

                OleDbParameter User = new OleDbParameter("PV_NICK_USER", OleDbType.VarChar);
                User.Direction = ParameterDirection.Input;
                User.Value = this.Nick_User;
                cmd.Parameters.Add(User);

                OleDbParameter DateP = new OleDbParameter("PD_DATE_PROGRAMED", OleDbType.Date);
                DateP.Direction = ParameterDirection.Input;
                DateP.Value = this.Date_Programed;
                cmd.Parameters.Add(DateP);

                OleDbParameter DateU = new OleDbParameter("PD_DATE_UPLOAD", OleDbType.Date);
                DateU.Direction = ParameterDirection.Input;
                DateU.Value = this.Date_Upload;
                cmd.Parameters.Add(DateU);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = this.IsValid;
                cmd.Parameters.Add(IsValid);

                cmd.ExecuteNonQuery();
                
                objConn.Close();
            }
            catch(Exception ex)
            {
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
            }
            finally
            {
                objConn.Close();
            }
        }

        public void UpdateYtbl_ProgCorte()
        {
            OleDbConnection objConn = conn.Conn();
            try
            {
                conn = new Connection();

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEPROGCORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter IdProgCorte = new OleDbParameter("PN_IDPROGCORTE", OleDbType.BigInt);
                IdProgCorte.Direction = ParameterDirection.Input;
                IdProgCorte.Value = this.Id;
                cmd.Parameters.Add(IdProgCorte);

                OleDbParameter Cparty = new OleDbParameter("PV_ISVALID", OleDbType.BigInt);
                Cparty.Direction = ParameterDirection.Input;
                Cparty.Value = this.IsValid;
                cmd.Parameters.Add(Cparty);

                OleDbParameter Account = new OleDbParameter("PD_DATEPROGRAMED", OleDbType.BigInt);
                Account.Direction = ParameterDirection.Input;
                Account.Value = this.Date_Programed;
                cmd.Parameters.Add(Account);

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

        public List<Ytbl_ProgCorteModels> SelectYtbl_ProgCorte()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROGCORTE";
            
            OleDbCommand cmd = new OleDbCommand();
            
            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            List<Ytbl_ProgCorteModels> cortesProgramados = new List<Ytbl_ProgCorteModels>();
            try
            {
                if (myReader.HasRows)
                { 
                    while(myReader.Read())
                    {
                        Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();
                        RecordCount++;
                        
                        ProgCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        ProgCorte.Document_Name = myReader.GetString(1).ToString();
                        ProgCorte.Customer_Number_Upload = myReader.GetString(2).ToString();
                        ProgCorte.Nick_User = myReader.GetString(3).ToString();
                        ProgCorte.Date_Programed = DateTime.Parse(myReader.GetDateTime(4).ToString());
                        ProgCorte.Date_Upload = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        ProgCorte.IsValid = myReader.GetString(6).ToString();

                        cortesProgramados.Add(ProgCorte);
                    }
                }

                return cortesProgramados;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return cortesProgramados;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public Ytbl_ProgCorteModels SelectYtbl_ProgCorte(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROGCORTE where id ="+id;

            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        
                        RecordCount++;

                        ProgCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        ProgCorte.Document_Name = myReader.GetString(1).ToString();
                        ProgCorte.Customer_Number_Upload = myReader.GetString(2).ToString();
                        ProgCorte.Nick_User = myReader.GetString(3).ToString();
                        ProgCorte.Date_Programed = DateTime.Parse(myReader.GetDateTime(4).ToString());
                        ProgCorte.Date_Upload = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        ProgCorte.IsValid = myReader.GetString(6).ToString();

                    }
                }

                return ProgCorte;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return ProgCorte;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }


    }
}