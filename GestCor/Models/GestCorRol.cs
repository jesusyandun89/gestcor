using GestCor.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class GestCorRol
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre del Rol")]
        public string NameRol { get; set; }
        [Required]
        [Display(Name = "Es valido")]
        public string IsValid { get; set; }
        [Required]
        [Display(Name = "Descripción del rol")]
        public string Description { get; set; }

        Connection conn;

        public bool SaveGestCorRoles()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTROLGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Module = new OleDbParameter("PV_NAME_ROL", OleDbType.VarChar);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.NameRol;
                cmd.Parameters.Add(Module);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = this.IsValid;
                cmd.Parameters.Add(IsValid);

                OleDbParameter Descrip = new OleDbParameter("PV_DESCRIPTION", OleDbType.VarChar);
                Descrip.Direction = ParameterDirection.Input;
                Descrip.Value = this.Description;
                cmd.Parameters.Add(Descrip);

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

        public bool UpdateGestCorRoles(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEROLGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("VI_ID", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = id;
                cmd.Parameters.Add(Id);

                OleDbParameter Module = new OleDbParameter("PV_NAME_ROL", OleDbType.VarChar);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.NameRol;
                cmd.Parameters.Add(Module);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = this.IsValid;
                cmd.Parameters.Add(IsValid);

                OleDbParameter Descrip = new OleDbParameter("PV_DESCRIPTION", OleDbType.VarChar);
                Descrip.Direction = ParameterDirection.Input;
                Descrip.Value = this.Description;
                cmd.Parameters.Add(Descrip);

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

        public List<GestCorRol> GetRoles()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_ROLGESTCOR order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<GestCorRol> RolesList = new List<GestCorRol>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        GestCorRol Rol = new GestCorRol();

                        Rol.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Rol.NameRol = myReader.GetString(1);
                        Rol.IsValid = myReader.GetString(2);
                        Rol.Description = myReader.GetString(3);

                        RolesList.Add(Rol);
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

        public GestCorRol GetRolesById(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_ROLGESTCOR where id ="+id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            GestCorRol Rol = new GestCorRol();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Rol.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Rol.NameRol = myReader.GetString(1);
                        Rol.IsValid = myReader.GetString(2);
                        Rol.Description = myReader.GetString(3);
                        
                    }
                }

                return Rol;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return Rol;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }
    }
}