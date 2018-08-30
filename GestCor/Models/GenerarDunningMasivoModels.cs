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
        private List<GenerarDunningMasivo> dunningMasivoList = new List<GenerarDunningMasivo>();
        Connection conn;

        public bool saveDunningMasivo()
        {
            try
            {
                for(int i = 0; i < dunningMasivoList.Count(); i++)
                {
                    
                }
                return true;
            }
            catch(Exception ex)
            {
                Logs.WriteErrorLog("Error al insertar datos||GenerarDunningMasivoModels.class||saveDunningMasivo||error: " + ex.ToString());
                return false;
            }
        }

        public bool truncateDunningMasico()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error al insertar datos||GenerarDunningMasivoModels.class||truncateDunningMasico||error: " + ex.ToString());
                return false;
            }
        }

        public List<GenerarDunningMasivo> getDunningMasivo()
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