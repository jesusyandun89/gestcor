using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace msgprepagosatelital.Clases
{
    public class Connection
    {
        public OleDbConnection Conn()
        {
            OleDbConnection conn;

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Titan"].ConnectionString;

                OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);

                conn = new OleDbConnection();
            
                return conn;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en conexión de base de datos||" + ex.ToString());
                return conn = new OleDbConnection("falso");
            }
        }
    }
}