using System;
using System.Configuration;
using System.Data.OleDb;

namespace msgprepagosatelital.Clases
{
    public class Connection
    {
        public OleDbConnection Conn()
        {
            string valCon = "provider=OraOLEDB.Oracle;Data Source = DEARROLLONEW; User ID = bsdesa; Password = com123desa;";

            OleDbConnection conn = new OleDbConnection(valCon);

            try
            {
                conn.Open();
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