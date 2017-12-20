using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using GestCor.Clases;

namespace GestCor.Models
{
    public class ControlGestCorService
    {
        private enum SimpleServiceCustomCommands
        { StopWorker = 128, RestartWorker, CheckWorker };

        private string service = ConfigurationManager.AppSettings.Get("service");

        public async Task run_async(bool activeProcess)
        {
            if (activeProcess)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
        public string getStatus()
        {
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == service)
                    {
                        ServiceController sc = new ServiceController(service);

                        if (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            Logs.WriteErrorLog("El servicio se encuentra detenido");
                            return ("El servicio se encuentra detenido");
                        }

                        if (sc.Status == ServiceControllerStatus.Running)
                        {
                            Logs.WriteErrorLog("El servicio se encuentra ejecutandose con normalidad");
                            return ("El servicio se encuentra ejecutandose con normalidad");
                        }
                    }
                }
                Logs.WriteErrorLog("El servicio no existe en el servidor");
                return ("El servicio no existe en el servidor");
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en la consulta del estado del servicio: " + ex.ToString());
                return ("Error en la consulta del estado del servicio");
            }

        }

        private bool Play()
        {
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == service)
                    {
                        ServiceController sc = new ServiceController(service);

                        sc.Start();
                        while (sc.Status == ServiceControllerStatus.Stopped)
                        {
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                        Logs.WriteErrorLog("El servicio ha sido iniciado satisfacoriamente");
                        return true;
                    }
                }
                Logs.WriteErrorLog("El servicio posiblemente no existe en el servidor");
                return false;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en la activación del servicio: " + ex.ToString());
                return false;
            }
        }

        private bool Stop()
        {
            try
            {
                ServiceController[] scServices = ServiceController.GetServices();

                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == service)
                    {
                        ServiceController sc = new ServiceController(service);

                        sc.Stop();
                        while (sc.Status != ServiceControllerStatus.Stopped)
                        {
                            Thread.Sleep(1000);
                            sc.Refresh();
                        }
                        Logs.WriteErrorLog("El servicio ha sido detenido satisfacoriamente");
                        return true;
                    }
                }
                Logs.WriteErrorLog("El servicio posiblemente no existe en el servidor");
                return false;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error al detener el servicio: " + ex.ToString());
                return false;
            }
        }
    }
}