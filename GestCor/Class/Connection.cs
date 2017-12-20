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
            string valCon = ConfigurationManager.ConnectionStrings["Titan"].ConnectionString;
            //string valCon = "provider=OraOLEDB.Oracle;Data Source = DEARROLLONEW; User ID = bsdesa; Password = com123desa;";

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

    }
}