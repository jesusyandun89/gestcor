using msgprepagosatelital.Clases;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class Ytbl_ProgCorteModels
    {
        public int? Id { get; set; }
        public string Document_Name { get; set; }
        public string Customer_Number_Upload { get; set; }
        public string Nick_User { get; set; }
        public DateTime Date_Programed { get; set; }
        public DateTime Date_Upload { get; set; }
        public string IsValid { get; set; }

        Connection conn;

        List<Ytbl_DetalleProgCorteModels> detalleCorte = new List<Ytbl_DetalleProgCorteModels>();

        public void SaveYtbl_ProgCorte()
        {
            conn = new Connection();
            OracleConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTPROGCORTE";
                objConn.Open();
                OracleCommand cmd = new OracleCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter Document = new OracleParameter("PN_DOCUMENT_NAME", OracleDbType.Varchar2);
                Document.Direction = ParameterDirection.Input;
                Document.Value = this.Document_Name;
                cmd.Parameters.Add(Document);

                OracleParameter Customers = new OracleParameter("PN_CUSTOMES_NUMBER_UPLOAD", OracleDbType.Varchar2);
                Customers.Direction = ParameterDirection.Input;
                Customers.Value = this.Customer_Number_Upload;
                cmd.Parameters.Add(Customers);

                OracleParameter User = new OracleParameter("PV_NICK_USER", OracleDbType.Varchar2);
                User.Direction = ParameterDirection.Input;
                User.Value = this.Nick_User;
                cmd.Parameters.Add(User);

                OracleParameter DateP = new OracleParameter("PD_DATE_PROGRAMED", OracleDbType.Date);
                DateP.Direction = ParameterDirection.Input;
                DateP.Value = this.Date_Programed;
                cmd.Parameters.Add(DateP);

                OracleParameter DateU = new OracleParameter("PD_DATE_UPLOAD", OracleDbType.Date);
                DateU.Direction = ParameterDirection.Input;
                DateU.Value = this.Date_Upload;
                cmd.Parameters.Add(DateU);

                OracleParameter IsValid = new OracleParameter("PV_ISVALID", OracleDbType.Varchar2);
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
            OracleConnection objConn = conn.Conn();
            try
            {
                conn = new Connection();

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEPROGCORTE";
                objConn.Open();
                OracleCommand cmd = new OracleCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter IdProgCorte = new OracleParameter("PN_IDPROGCORTE", OracleDbType.Int64);
                IdProgCorte.Direction = ParameterDirection.Input;
                IdProgCorte.Value = this.Id;
                cmd.Parameters.Add(IdProgCorte);

                OracleParameter Cparty = new OracleParameter("PV_ISVALID", OracleDbType.Int64);
                Cparty.Direction = ParameterDirection.Input;
                Cparty.Value = this.IsValid;
                cmd.Parameters.Add(Cparty);

                OracleParameter Account = new OracleParameter("PD_DATEPROGRAMED", OracleDbType.Int64);
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
            OracleConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROGCORTE";
            objConn.Open();
            OracleCommand cmd = new OracleCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;

            OracleDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            List<Ytbl_ProgCorteModels> cortesProgramados = new List<Ytbl_ProgCorteModels>();
            try
            {
                if(myReader.HasRows)
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


    }
}