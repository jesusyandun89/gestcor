using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace GestCor.Clases
{
    public class Connection
    {
        public OleDbConnection Conn()
        {
            string valCon = "provider=OraOLEDB.Oracle;Data Source = DEARROLLONEW; User ID = bsdesa; Password = com123desa;";

            OleDbConnection conn = new OleDbConnection(valCon);

            try
            {
                return conn;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en conexión de base de datos||" + ex.ToString());
                return conn;
            }
        }

        public bool getStatusInstance()
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select YPKG_WEBCORTES.FNGETDUNNINGCOMPLETE(1) from dual";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int statusDunning = 0;
            string value = "";
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        value = myReader.GetValue(0).ToString();
                        if (statusDunning == 1)
                            return false;
                        else
                            return true;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en llamada del API: " + ex.ToString());
                objConn.Close();
                return true;
            }
            finally
            {
                objConn.Close();
            }
        }
    }
}