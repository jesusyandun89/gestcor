using System;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace msgprepagosatelital.Clases
{
    public class Connection
    {
        public OracleConnection Conn()
        {
            OracleConnection conn;

            try
            {
                //var connectionString = ConfigurationManager.ConnectionStrings["SaveProgramaCorteContext"].ConnectionString;

                ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

                string valCon = "Data Source = DEARROLLONEW; User ID = bsdesa; Password = com123desa";


                conn = new OracleConnection();
                conn.ConnectionString = valCon;
            
                return conn;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en conexión de base de datos||" + ex.ToString());
                return conn = new OracleConnection("falso");
            }
        }
    }
}