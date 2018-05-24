using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.Text;
using GestCor.Clases;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

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

        internal bool ValidateUser(string usuario)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_USERSGESTCOR where USERGESTCOR = '"+usuario+"' and ISVALID = 'Y' and dateto is null";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return false;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public String ValidaRoles(string userId, string password, string userRol)
        {
            string DomainUser = ConfigurationManager.AppSettings.Get("DomainUser");

            String domainAndUsername = DomainUser + @"\" + userId;
            
            String valor = GetGroups(domainAndUsername, userRol, password);
            return valor;
        }

        internal int ValidaModulo(string usuario, string userRol)
        {
            List<NameModule> listPermissos = getAuthorizeByUser(usuario);

            var rolUser = from module in listPermissos
                          where module.nameModule == userRol
                          select module;

            int valueReturn = -1;
            foreach (var item in rolUser)
            {
                if (item.nameModule == userRol)
                    valueReturn = 1;
                else
                    valueReturn = -1;
            }
            return valueReturn;
        }

        private List<NameModule> getAuthorizeByUser(string usuario)
        {
            Connection conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select d.NAME_MODULE from YTBL_USERSGESTCOR a, YTBL_ROLGESTCOR b, YTBL_PROFILEGESTCOR c, YTBL_MODULESGESTCOR d"
                    + " where USERGESTCOR = '" + usuario + "' and a.id_rol = b.id and a.id_rol = c.id_rol and b.id = c.ID_ROL and c.ID_MODULE = d.id"
                    + " and a.ISVALID = 'Y' and b.ISVALID = 'Y' and c.ISVALID = 'Y' and d.ISVALID = 'Y'";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<NameModule> ModuleList = new List<NameModule>();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        NameModule module = new NameModule();

                        module.nameModule = myReader.GetString(0);

                        ModuleList.Add(module);
                    }
                }

                return ModuleList;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return ModuleList;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
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

    public class NameModule
    {
        public string nameModule { get; set; }
    }
}