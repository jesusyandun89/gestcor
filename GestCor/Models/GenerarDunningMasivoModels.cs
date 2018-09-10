using GestCor.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace GestCor.Models
{
    public class GenerarDunningMasivoModels
    {
        public List<GenerarDunningMasivo> dunningMasivoList = new List<GenerarDunningMasivo>();
        Connection conn;

        public bool saveDunningMasivo()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                for(int i = 0; i < dunningMasivoList.Count(); i++)
                {
                    // Calling sp_processdata defined inside PKG PKG_MANAGER

                    string commText = "YPKG_WEBCORTES.YPRD_INSERTNOGENERACORTE";
                    objConn.Open();
                    OleDbCommand cmd = new OleDbCommand(commText, objConn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    OleDbParameter Id = new OleDbParameter("PN_ACCOUNT_ID", OleDbType.BigInt);
                    Id.Direction = ParameterDirection.Input;
                    Id.Value = dunningMasivoList[i].ACCOUNT_ID;
                    cmd.Parameters.Add(Id);

                    cmd.ExecuteNonQuery();

                    objConn.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                Logs.WriteErrorLog("Error al insertar datos||GenerarDunningMasivoModels.class||saveDunningMasivo||error: " + ex.ToString());
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public bool truncateDunningMasico()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_DELETENOGENERACORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Id = new OleDbParameter("PN_FLAG", OleDbType.Integer);
                Id.Direction = ParameterDirection.Input;
                Id.Value = 1;
                cmd.Parameters.Add(Id);

                cmd.ExecuteNonQuery();

                objConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error al truncar datos||GenerarDunningMasivoModels.class||truncateDunningMasico||error: " + ex.ToString());
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public List<GenerarDunningMasivo> getDunningMasivo()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from Y_NOGENERARDUNINGSMASIVO";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<GenerarDunningMasivo> dunningMasivoList = new List<GenerarDunningMasivo>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        GenerarDunningMasivo dunning = new GenerarDunningMasivo();

                        dunning.ACCOUNT_ID = Int64.Parse(myReader.GetDecimal(0).ToString());

                        dunningMasivoList.Add(dunning);
                    }
                }

                return dunningMasivoList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||GenerarDunningMasivo||getDunningMasivo||error: " + ex.ToString());
                return dunningMasivoList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

    }

    public class GenerarDunningMasivo
    {
        public Int64 ACCOUNT_ID { get; set; }
    }
}