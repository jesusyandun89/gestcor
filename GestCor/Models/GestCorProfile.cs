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
    public class GestCorProfile
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del perfil")]
        public string NameProfile { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int RolId { get; set; }

        [Display(Name = "Nombre del rol")]
        public string NameRol { get; set; }

        [Required]
        [Display(Name = "Módulo")]
        public int IdModule { get; set; }

        [Display(Name = "Nombre del módulo")]
        public string NameModule { get; set; }

        [Required]
        [Display(Name = "Es válido")]
        public string IsValid { get; set; }

        public bool SaveProfile()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTPROFILEGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Profile = new OleDbParameter("PV_NAME_PROFILE", OleDbType.VarChar);
                Profile.Direction = ParameterDirection.Input;
                Profile.Value = this.NameProfile;
                cmd.Parameters.Add(Profile);

                OleDbParameter RolId = new OleDbParameter("PN_ID_ROL", OleDbType.Integer);
                RolId.Direction = ParameterDirection.Input;
                RolId.Value = this.RolId;
                cmd.Parameters.Add(RolId);

                OleDbParameter Module = new OleDbParameter("PN_ID_MODULE", OleDbType.Integer);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.IdModule;
                cmd.Parameters.Add(Module);

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
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public bool UpdateProfile(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEPROFILEGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("VI_ID", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = id;
                cmd.Parameters.Add(Id);

                OleDbParameter Module = new OleDbParameter("PV_NAME_PROFILE", OleDbType.VarChar);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.NameProfile;
                cmd.Parameters.Add(Module);

                OleDbParameter RolId = new OleDbParameter("PN_ID_ROL", OleDbType.VarChar);
                RolId.Direction = ParameterDirection.Input;
                RolId.Value = this.RolId;
                cmd.Parameters.Add(RolId);

                OleDbParameter ModuleId = new OleDbParameter("PN_ID_MODULO", OleDbType.VarChar);
                ModuleId.Direction = ParameterDirection.Input;
                ModuleId.Value = this.IdModule;
                cmd.Parameters.Add(ModuleId);

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
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public List<GestCorProfile> GetProfiles()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROFILEGESTCOR order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<GestCorProfile> ProfilesList = new List<GestCorProfile>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        GestCorProfile Profile = new GestCorProfile();

                        Profile.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Profile.NameProfile = myReader.GetString(1);
                        Profile.RolId = int.Parse(myReader.GetDecimal(2).ToString());
                        Profile.NameRol = getNameRol(int.Parse(myReader.GetDecimal(2).ToString()));
                        Profile.IdModule = int.Parse(myReader.GetDecimal(3).ToString());
                        Profile.NameModule = getNameModule(int.Parse(myReader.GetDecimal(3).ToString()));
                        Profile.IsValid = myReader.GetString(4);

                        ProfilesList.Add(Profile);
                    }
                }

                return ProfilesList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return ProfilesList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public GestCorProfile GetProfilesById(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROFILEGESTCOR  where id = " + id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            GestCorProfile Profile = new GestCorProfile();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Profile.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Profile.NameProfile = myReader.GetString(1);
                        Profile.RolId = int.Parse(myReader.GetDecimal(2).ToString());
                        Profile.NameRol = getNameRol(int.Parse(myReader.GetDecimal(2).ToString()));
                        Profile.IdModule = int.Parse(myReader.GetDecimal(3).ToString());
                        Profile.NameModule = getNameModule(int.Parse(myReader.GetDecimal(3).ToString()));
                        Profile.IsValid = myReader.GetString(4);
                    }
                }

                return Profile;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return Profile;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public List<SelectListItem> getModulesAvaliable(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select id, NAME_MODULE, decode(id, " + id + ", 'true','false') from YTBL_MODULESGESTCOR where ISVALID = 'Y' order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<SelectListItem> ModulesList = new List<SelectListItem>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        SelectListItem module = new SelectListItem();
                        module.Value = myReader.GetValue(0).ToString();
                        module.Text = myReader.GetValue(1).ToString();
                        module.Selected = bool.Parse(myReader.GetValue(2).ToString());

                        ModulesList.Add(module);
                    }
                }

                return ModulesList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return ModulesList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public string getNameModule(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select NAME_MODULE from YTBL_MODULESGESTCOR where id =" + id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            string nameModule = "";
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                        nameModule = myReader.GetString(0);
                    }
                }

                return nameModule;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return nameModule;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public List<SelectListItem> getRolesAvaliable(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select id, NAME_ROL, decode(id," + id + " , 'true','false') from YTBL_ROLGESTCOR where ISVALID = 'Y'  order by id desc";

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
                        rol.Value = myReader.GetValue(0).ToString();
                        rol.Text = myReader.GetValue(1).ToString();
                        rol.Selected = bool.Parse(myReader.GetValue(2).ToString());

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

        public string getNameRol(int id)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select NAME_ROL from YTBL_ROLGESTCOR where id =" + id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            string nameRol = "";
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {

                        nameRol = myReader.GetString(0);
                    }
                }

                return nameRol;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return nameRol;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

    }
        
}