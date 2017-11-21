using System;
using System.IO;

namespace msgprepagosatelital.Clases
{
    class Logs
    {
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                DateTime nameFile = DateTime.Today;
                string Name = nameFile.ToString("MM-dd-yy");
                var fileOpen = File.Open("c:/SMSLogs/logUserApp-" + Name + ".txt", FileMode.Append);    
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
                DateTime nameFile = DateTime.Today;
                string Name = nameFile.ToString("MM-dd-yy");
                var fileOpen = File.Open("c:/SMSLogs/logUserApp-" + Name + ".txt", FileMode.Append);
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
