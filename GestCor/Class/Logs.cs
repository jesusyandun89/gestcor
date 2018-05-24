using System;
using System.IO;
using System.Configuration;


namespace GestCor.Clases
{
    class Logs
    {
        private static string DirectoryLogs = ConfigurationManager.AppSettings.Get("DirectoryLogs").ToString();
        public static void WriteErrorLog(Exception ex)
        {

            StreamWriter sw = null;
            try
            {
                if (!System.IO.Directory.Exists(DirectoryLogs))
                {
                    System.IO.Directory.CreateDirectory(DirectoryLogs);
                }
                DateTime nameFile = DateTime.Today;
                string Name = nameFile.ToString("MM-dd-yy");
                var fileOpen = File.Open(DirectoryLogs+ "/logUserApp-" + Name + ".txt", FileMode.Append);    
                sw = new StreamWriter(fileOpen);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }

        }
        public static void WriteErrorLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                if (!System.IO.Directory.Exists(DirectoryLogs))
                {
                    System.IO.Directory.CreateDirectory(DirectoryLogs);
                }
                DateTime nameFile = DateTime.Today;
                string Name = nameFile.ToString("MM-dd-yy");
                var fileOpen = File.Open(DirectoryLogs + "/logUserApp-" + Name + ".txt", FileMode.Append);
                sw = new StreamWriter(fileOpen);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }

        }
    }
}
