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
    public class UserGestCor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string NickUser { get; set; }

        [Required]
        [Display(Name = "Rol del usuario")]
        public int IdRol { get; set; }

        public string NameRol { get; set; }

        [Required]
        [Display(Name = "Es valido")]
        public string IsValid { get; set; }
        

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        Connection conn;

        public bool SaveUser()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTUSERSGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter NameUser = new OleDbParameter("PV_USERGESTCOR", OleDbType.VarChar);
                NameUser.Direction = ParameterDirection.Input;
                NameUser.Value = this.NickUser;
                cmd.Parameters.Add(NameUser);

                OleDbParameter IdRol = new OleDbParameter("PN_ID_ROL", OleDbType.VarChar);
                IdRol.Direction = ParameterDirection.Input;
                IdRol.Value = this.IdRol;
                cmd.Parameters.Add(IdRol);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = this.IsValid;
                cmd.Parameters.Add(IsValid);

                OleDbParameter Datef = new OleDbParameter("PD_DATEFROM", OleDbType.Date);
                Datef.Direction = ParameterDirection.Input;
                Datef.Value = DateTime.Now;
                cmd.Parameters.Add(Datef);
                

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

        public bool UpdateUser(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEUSERSGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("VI_ID", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = id;
                cmd.Parameters.Add(Id);

                OleDbParameter NameUser = new OleDbParameter("PV_USERGESTCOR", OleDbType.VarChar);
                NameUser.Direction = ParameterDirection.Input;
                NameUser.Value = this.NickUser;
                cmd.Parameters.Add(NameUser);

                OleDbParameter Rol = new OleDbParameter("PN_ID_ROL", OleDbType.Integer);
                Rol.Direction = ParameterDirection.Input;
                Rol.Value = this.IdRol;
                cmd.Parameters.Add(Rol);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = this.IsValid;
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

        public List<UserGestCor> GetUsers()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_USERSGESTCOR order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<UserGestCor> UsersList = new List<UserGestCor>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        UserGestCor User = new UserGestCor();

                        Rol RolName = new Rol();

                        User.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        User.NickUser = myReader.GetString(1);
                        User.IdRol = int.Parse(myReader.GetDecimal(2).ToString());
                        User.NameRol = RolName.getNameRol(int.Parse(myReader.GetDecimal(2).ToString()));
                        User.IsValid = myReader.GetString(3);
                        
                        try
                        {
                            User.DateFrom = myReader.GetDateTime(4);
                        }
                        catch (Exception ex)
                        {
                            User.DateFrom = null;
                        }
                        try
                        {
                            User.DateTo = myReader.GetDateTime(5);
                        }
                        catch (Exception ex)
                        {
                            User.DateTo = null;
                        }
                        

                        UsersList.Add(User);
                    }
                }

                return UsersList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return UsersList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public UserGestCor GetUsersById(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_USERSGESTCOR where id ="+id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            UserGestCor User = new UserGestCor();
            Rol RolName = new Rol();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                        User.Id = int.Parse(myReader.GetDecimal(2).ToString());
                        User.NickUser = myReader.GetString(1);
                        User.IdRol = int.Parse(myReader.GetDecimal(2).ToString());
                        User.NameRol = RolName.getNameRol(int.Parse(myReader.GetDecimal(2).ToString()));
                        User.IsValid = myReader.GetString(3);
                        try
                        {
                            User.DateFrom = myReader.GetDateTime(4);
                        }
                        catch (Exception ex)
                        {
                            User.DateFrom = null;
                        }
                        try
                        {
                            User.DateTo = myReader.GetDateTime(5);
                        }
                        catch (Exception ex)
                        {
                            User.DateTo = null;
                        }

                    }
                }

                return User;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return User;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }
    }
}