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
    public class GestCorModules
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre del modulo")]
        public string NameModule { get; set; }
        [Required]
        [Display(Name = "Is valido")]
        public string IsValid { get; set; }
        [Required]
        [Display(Name = "Descripción del modulo")]
        public string Description { get; set; }

        Connection conn;

        public bool SaveModule()
        {
             conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_INSERTMODULESGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Module = new OleDbParameter("PV_NAME_MODULE", OleDbType.VarChar);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.NameModule;
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

        public bool UpdateModule(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEMODULESGESTCOR";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("VI_ID", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = id;
                cmd.Parameters.Add(Id);

                OleDbParameter Module = new OleDbParameter("PV_NAME_MODULE", OleDbType.VarChar);
                Module.Direction = ParameterDirection.Input;
                Module.Value = this.NameModule;
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

        public List<GestCorModules> GetModules()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_MODULESGESTCOR order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<GestCorModules> ModulesList = new List<GestCorModules>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        GestCorModules Module = new GestCorModules();

                        Module.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Module.NameModule = myReader.GetString(1);
                        Module.IsValid = myReader.GetString(2);
                        Module.Description = myReader.GetString(3);

                        ModulesList.Add(Module);
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

        public GestCorModules GetModulesById(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_MODULESGESTCOR where id = "+id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            GestCorModules Module = new GestCorModules();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Module.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        Module.NameModule = myReader.GetString(1);
                        Module.IsValid = myReader.GetString(2);
                        Module.Description = myReader.GetString(3);

                        return Module;
                    }
                }

                return Module;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return Module;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }


    }
}