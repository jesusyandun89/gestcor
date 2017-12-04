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
        [Required]
        [Display(Name = "Nombre del destinatario")]
        public string Name { get; set; }
        [Display(Name = "Email destinatario")]
        [Required]
        public string Correo { get; set; }
        [Display(Name = "Es válido")]
        [Required]
        public string IsValid { get; set; }
        [Display(Name = "Sistema de notificaciones")]
        [Required]
        public string System { get; set; }
        [Display(Name = "Fecha de creación")]
        [Required]
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

                OleDbParameter Name = new OleDbParameter("PV_NAME", OleDbType.VarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = CorreoNotificacionesModels.Name;
                cmd.Parameters.Add(Name);

                OleDbParameter Correo = new OleDbParameter("PV_CORREO", OleDbType.VarChar);
                Correo.Direction = ParameterDirection.Input;
                Correo.Value = CorreoNotificacionesModels.Correo;
                cmd.Parameters.Add(Correo);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = CorreoNotificacionesModels.IsValid;
                cmd.Parameters.Add(IsValid);

                OleDbParameter System = new OleDbParameter("PV_SYSTEM", OleDbType.VarChar);
                System.Direction = ParameterDirection.Input;
                System.Value = CorreoNotificacionesModels.System;
                cmd.Parameters.Add(System);

                OleDbParameter Fecha = new OleDbParameter("PD_FECHA", OleDbType.Date);
                Fecha.Direction = ParameterDirection.Input;
                Fecha.Value = CorreoNotificacionesModels.Fecha;
                cmd.Parameters.Add(Fecha);

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
                        CorreoNotificaciones.System = myReader.GetString(4).ToString();
                        CorreoNotificaciones.Fecha = DateTime.Parse(myReader.GetDateTime(5).ToString());

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
                        CorreoNotificaciones.System = myReader.GetString(4).ToString();
                        CorreoNotificaciones.Fecha = DateTime.Parse(myReader.GetDateTime(5).ToString());

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

                string commText = "YPKG_WEBCORTES.YPRD_UPDATENOTIFICACIONES";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("PN_ID", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = model.Id;
                cmd.Parameters.Add(Id);

                OleDbParameter Name = new OleDbParameter("PV_NAME", OleDbType.VarChar);
                Name.Direction = ParameterDirection.Input;
                Name.Value = model.Name;
                cmd.Parameters.Add(Name);

                OleDbParameter Correo = new OleDbParameter("PV_CORREO", OleDbType.VarChar);
                Correo.Direction = ParameterDirection.Input;
                Correo.Value = model.Correo;
                cmd.Parameters.Add(Correo);

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