using GestCor.Clases;
using GestCor.Class;
using System;
using System.Web;
using System.Web.Mvc;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        try
        { 
            HttpContext ctx = System.Web.HttpContext.Current;
            if (filterContext == null)
            {
                throw new ArgumentNullException("AuthorizeFilterAttribute");
            }

            ValidationUser MO = new ValidationUser();
            String usuario = filterContext.HttpContext.Session["usuario"].ToString();
            String password = filterContext.HttpContext.Session["password"].ToString();

            String valor = MO.ValidaRoles(usuario, password);
            int roles = valor.IndexOf("Mensajeria Prepago Satelital");
            if (roles == -1)
            {
                filterContext.Result = new ViewResult { ViewName = "ErrorAcceso" };
            }
        }
        catch (Exception ex)
        {
            Logs.WriteErrorLog("Error en ingresar a dicha página: " + ex.ToString());
            filterContext.Result = new ViewResult { ViewName = "ErrorAcceso" };
        }
    }

    public int Authorize(string usuario, string password)
    {
        ValidationUser MO = new ValidationUser();

        String valor = MO.ValidaRoles(usuario, password);
        int roles;
        int credIncorrectas;
        if ((credIncorrectas = valor.LastIndexOf("Error de inicio de sesión: nombre de usuario desconocido o contraseña incorrecta.")) != -1)
        {
            return 2;
        }
        else if((roles = valor.LastIndexOf("GeoreferenciacionFSM")) != -1)
        {
            return 0;
        }
        else if(credIncorrectas == -1 && roles == -1)
        {
            return 1;
        }
        else
        {
            return -1;
        }
        
    }
}