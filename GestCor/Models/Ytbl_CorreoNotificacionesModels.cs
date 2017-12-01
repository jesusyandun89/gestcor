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
    public class Ytbl_CorreoNotificacionesModels
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del destinatario")]
        public string Name { get; set; }
        [Display(Name = "Email destinatario")]
        public string Correo { get; set; }
        [Display(Name = "Es valido")]
        public string IsValid { get; set; }
        [Display(Name = "Sistema de notificaciones")]
        public string System { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime Fecha { get; set; }

        Connection conn;

        public void SaveYtbl_CorreoNotificaciones(Ytbl_CorreoNotificacionesModels CorreoNotificacionesModels)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTNOTIFICACIONES";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Document = new OleDbParameter("PV_NAME", OleDbType.VarChar);
                Document.Direction = ParameterDirection.Input;
                Document.Value = CorreoNotificacionesModels.Name;
                cmd.Parameters.Add(Document);

                OleDbParameter Customers = new OleDbParameter("PV_CORREO", OleDbType.VarChar);
                Customers.Direction = ParameterDirection.Input;
                Customers.Value = CorreoNotificacionesModels.Correo;
                cmd.Parameters.Add(Customers);

                OleDbParameter User = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                User.Direction = ParameterDirection.Input;
                User.Value = CorreoNotificacionesModels.IsValid;
                cmd.Parameters.Add(User);

                OleDbParameter DateP = new OleDbParameter("PV_SYSTEM", OleDbType.VarChar);
                DateP.Direction = ParameterDirection.Input;
                DateP.Value = CorreoNotificacionesModels.System;
                cmd.Parameters.Add(DateP);

                OleDbParameter DateU = new OleDbParameter("PD_FECHA", OleDbType.Date);
                DateU.Direction = ParameterDirection.Input;
                DateU.Value = CorreoNotificacionesModels.Fecha;
                cmd.Parameters.Add(DateU);

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
        
        public List<Ytbl_CorreoNotificacionesModels> SelectCorreos()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_CORREONOTIFICACIONES";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            List<Ytbl_CorreoNotificacionesModels> correoNotificacionesLista = new List<Ytbl_CorreoNotificacionesModels>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Ytbl_CorreoNotificacionesModels CorreoNotificaciones = new Ytbl_CorreoNotificacionesModels();
                        RecordCount++;

                        CorreoNotificaciones.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        CorreoNotificaciones.Name = myReader.GetString(1).ToString();
                        CorreoNotificaciones.Correo = myReader.GetString(2).ToString();
                        CorreoNotificaciones.IsValid = myReader.GetString(3).ToString();
                        CorreoNotificaciones.System = myReader.GetDateTime(4).ToString();
                        CorreoNotificaciones.Fecha = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        CorreoNotificaciones.IsValid = myReader.GetString(6).ToString();

                        correoNotificacionesLista.Add(CorreoNotificaciones);
                    }
                }

                return correoNotificacionesLista;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return correoNotificacionesLista;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public Ytbl_CorreoNotificacionesModels SelectCorreosById(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_CORREONOTIFICACIONES where id ="+id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            Ytbl_CorreoNotificacionesModels CorreoNotificaciones = new Ytbl_CorreoNotificacionesModels();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        CorreoNotificaciones.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        CorreoNotificaciones.Name = myReader.GetString(1).ToString();
                        CorreoNotificaciones.Correo = myReader.GetString(2).ToString();
                        CorreoNotificaciones.IsValid = myReader.GetString(3).ToString();
                        CorreoNotificaciones.System = myReader.GetDateTime(4).ToString();
                        CorreoNotificaciones.Fecha = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        CorreoNotificaciones.IsValid = myReader.GetString(6).ToString();

                        return CorreoNotificaciones;
                    }
                }
                return CorreoNotificaciones;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return CorreoNotificaciones; ;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public void UpdateCorreo(Ytbl_CorreoNotificacionesModels model)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTNOTIFICACIONES";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("PN_DOCUMENT_NAME", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = model.Id;
                cmd.Parameters.Add(Id);

                OleDbParameter Document = new OleDbParameter("PN_DOCUMENT_NAME", OleDbType.VarChar);
                Document.Direction = ParameterDirection.Input;
                Document.Value = model.Name;
                cmd.Parameters.Add(Document);

                OleDbParameter Customers = new OleDbParameter("PN_CUSTOMES_NUMBER_UPLOAD", OleDbType.VarChar);
                Customers.Direction = ParameterDirection.Input;
                Customers.Value = model.Correo;
                cmd.Parameters.Add(Customers);

                OleDbParameter User = new OleDbParameter("PV_NICK_USER", OleDbType.VarChar);
                User.Direction = ParameterDirection.Input;
                User.Value = model.IsValid;
                cmd.Parameters.Add(User);

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