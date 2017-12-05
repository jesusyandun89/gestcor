using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.Text;

namespace GestCor.Class
{
    public class ValidationUser : AuthorizeAttribute
    {
        private String _path;
        private String _filterAttribute;
        string w_UserAD;

        //- Método que separa el dominio y el usuario
        public void UsuarioId(string p_Username)
        {
            string w_Usuario;
            string w_Dominio;
            char[] delimiterChars = { '\\' };
            try
            {
                string[] Datos = p_Username.Split(delimiterChars);
                w_Dominio = Datos[0];
                w_Usuario = Datos[1];
                w_UserAD = w_Usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        

        public String ValidaRoles(string userId, string password)
        {
            //HttpContext ctx = System.Web.HttpContext.Current;

            String domainAndUsername = "TVCABLEUIO" + @"\" + userId;
            //Administradores Gestion Mensajeria|Ingreso Campañas Gestion de Mensajeria
            String valor = GetGroups(domainAndUsername, Roles, password);
            return valor;
        }

        [HttpPost]
        public String GetGroups(String _path, String roles, String password)
        {
            StringBuilder groupNames = new StringBuilder();
            try
            {
               
                DirectoryEntry deEntry = new DirectoryEntry();
                
                deEntry.Username = _path;
                deEntry.Password = password;
                DirectorySearcher dsSearcher = new DirectorySearcher(deEntry);
                //bool bandera = false;            
                UsuarioId(_path);
                dsSearcher.Filter = "(SAMAccountName=" + w_UserAD.Trim() + ")";
                dsSearcher.PropertiesToLoad.Add("cn");
                SearchResult resultado = dsSearcher.FindOne();
                _path = resultado.Path;
                _filterAttribute = (String)resultado.Properties["cn"][0];

                DirectorySearcher search = new DirectorySearcher(_path);
                search.Filter = "(cn=" + _filterAttribute + ")";
                search.PropertiesToLoad.Add("memberOf");

            
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return groupNames.ToString();

        }
    }
}