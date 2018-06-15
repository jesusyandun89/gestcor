using GestCor.Clases;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GestCor.Models
{
    public class Ytbl_ProgCorteModels
    {
        [Display(Name = "Id del corte")]
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Nombre del documento")]
        public string Document_Name { get; set; }
        [Display(Name = "Cantidad de citems")]
        [Required]
        public string Customer_Number_Upload { get; set; }
        [Display(Name = "Usuario")]
        [Required]
        public string Nick_User { get; set; }
        [Display(Name = "Fecha programada")]
        [Required]
        public DateTime Date_Programed { get; set; }
        [Display(Name = "Fecha de creación")]
        [Required]
        public DateTime Date_Upload { get; set; }
        [Display(Name = "Es válido")]
        [Required]
        public string IsValid { get; set; }

        Connection conn;

        public static List<Ytbl_DetalleProgCorteModels> DetalleCorte { get; set; }

        public static List<Ytbl_ProgCorteModels> ListProgCorte { get; set; }

        public bool ExecuteSave(Ytbl_ProgCorteModels ListCorte)
        {
            Ytbl_DetalleProgCorteModels SaveDetalle = new Ytbl_DetalleProgCorteModels();
            try
            {

                if (SaveYtbl_ProgCorte(ListCorte) == false)
                    return false;

                int idProgCorte = SelectMaxId(ListCorte.Document_Name);

                Parallel.For(0, DetalleCorte.Count, i =>
                {
                    Ytbl_DetalleProgCorteModels CorteDetalle = new Ytbl_DetalleProgCorteModels();

                    CorteDetalle.id_ProgCorte = idProgCorte;
                    CorteDetalle.CpartyId = DetalleCorte[i].CpartyId;
                    CorteDetalle.CpartyAccountId = DetalleCorte[i].CpartyAccountId;
                    CorteDetalle.CitemId = DetalleCorte[i].CitemId;
                    CorteDetalle.FormaPago = DetalleCorte[i].FormaPago;
                    CorteDetalle.Ciudad = DetalleCorte[i].Ciudad;
                    CorteDetalle.BancoId = DetalleCorte[i].BancoId;
                    CorteDetalle.TipoNegocio = DetalleCorte[i].TipoNegocio;
                    CorteDetalle.EmpresaFacturadora = DetalleCorte[i].EmpresaFacturadora;
                    CorteDetalle.FieldV1 = DetalleCorte[i].FieldV1;
                    CorteDetalle.FieldV2 = DetalleCorte[i].FieldV2;
                    CorteDetalle.FieldN1 = DetalleCorte[i].FieldN1;
                    CorteDetalle.FieldN2 = DetalleCorte[i].FieldN2;
                    CorteDetalle.FieldD1 = DetalleCorte[i].FieldD1;
                    CorteDetalle.Status = DetalleCorte[i].Status;

                    SaveDetalle.SaveYtbl_DetalleProgCorte(CorteDetalle);
                });

                /*foreach (var item in DetalleCorte)
                {
                    Ytbl_DetalleProgCorteModels CorteDetalle = new Ytbl_DetalleProgCorteModels();

                    CorteDetalle.id_ProgCorte = idProgCorte;
                    CorteDetalle.CpartyId = item.CpartyId ;
                    CorteDetalle.CpartyAccountId = item.CpartyAccountId;
                    CorteDetalle.CitemId = item.CitemId;
                    CorteDetalle.FormaPago = item.FormaPago;
                    CorteDetalle.Ciudad = item.Ciudad;
                    CorteDetalle.BancoId = item.BancoId;
                    CorteDetalle.TipoNegocio = item.TipoNegocio;
                    CorteDetalle.EmpresaFacturadora = item.EmpresaFacturadora;
                    CorteDetalle.FieldV1 = item.FieldV1;
                    CorteDetalle.FieldV2 = item.FieldV2;
                    CorteDetalle.FieldN1 = item.FieldN1;
                    CorteDetalle.FieldN2 = item.FieldN2;
                    CorteDetalle.FieldD1 = item.FieldD1;
                    CorteDetalle.Status = item.Status;

                    SaveDetalle.SaveYtbl_DetalleProgCorte(CorteDetalle);

                }*/

                Ytbl_CorreoNotificacionesModels notificacion = new Ytbl_CorreoNotificacionesModels();

                notificacion.SendMailNotification(idProgCorte);

                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en ExecuteSave: " + ex.ToString());
                return false;
            }
            
        }

        public bool SaveYtbl_ProgCorte(Ytbl_ProgCorteModels ProgCorte)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            try
            {
                // Calling sp_processdata defined inside PKG PKG_MANAGER

                string commText = "YPKG_WEBCORTES.YPRD_INSERTPROGCORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter Document = new OleDbParameter("PN_DOCUMENT_NAME", OleDbType.VarChar);
                Document.Direction = ParameterDirection.Input;
                Document.Value = ProgCorte.Document_Name;
                cmd.Parameters.Add(Document);

                OleDbParameter Customers = new OleDbParameter("PN_CUSTOMES_NUMBER_UPLOAD", OleDbType.VarChar);
                Customers.Direction = ParameterDirection.Input;
                Customers.Value = ProgCorte.Customer_Number_Upload;
                cmd.Parameters.Add(Customers);

                OleDbParameter User = new OleDbParameter("PV_NICK_USER", OleDbType.VarChar);
                User.Direction = ParameterDirection.Input;
                User.Value = ProgCorte.Nick_User;
                cmd.Parameters.Add(User);

                OleDbParameter DateP = new OleDbParameter("PD_DATE_PROGRAMED", OleDbType.Date);
                DateP.Direction = ParameterDirection.Input;
                DateP.Value = ProgCorte.Date_Programed;
                cmd.Parameters.Add(DateP);

                OleDbParameter DateU = new OleDbParameter("PD_DATE_UPLOAD", OleDbType.Date);
                DateU.Direction = ParameterDirection.Input;
                DateU.Value = ProgCorte.Date_Upload;
                cmd.Parameters.Add(DateU);

                OleDbParameter IsValid = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                IsValid.Direction = ParameterDirection.Input;
                IsValid.Value = ProgCorte.IsValid;
                cmd.Parameters.Add(IsValid);

                cmd.ExecuteNonQuery();
                
                objConn.Close();
                return true;
            }
            catch(Exception ex)
            {
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public bool UpdateYtbl_ProgCorte(Ytbl_ProgCorteModels model)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            //-----
            try
            {
                conn = new Connection();

                string commText = "YPKG_WEBCORTES.YPRD_UPDATEPROGCORTE";
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand(commText, objConn);

                cmd.CommandType = CommandType.StoredProcedure;

                OleDbParameter IdProgCorte = new OleDbParameter("PN_IDPROGCORTE", OleDbType.BigInt);
                IdProgCorte.Direction = ParameterDirection.Input;
                IdProgCorte.Value = model.Id;
                cmd.Parameters.Add(IdProgCorte);

                OleDbParameter Cparty = new OleDbParameter("PV_ISVALID", OleDbType.VarChar);
                Cparty.Direction = ParameterDirection.Input;
                Cparty.Value = model.IsValid;
                cmd.Parameters.Add(Cparty);

                OleDbParameter Account = new OleDbParameter("PD_DATEPROGRAMED", OleDbType.Date);
                Account.Direction = ParameterDirection.Input;
                Account.Value = model.Date_Programed;
                cmd.Parameters.Add(Account);

                cmd.ExecuteNonQuery();
                objConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog("Error en insert: " + ex.ToString());
                objConn.Close();
                return false;
            }
            finally
            {
                objConn.Close();
            }
        }

        public List<Ytbl_ProgCorteModels> SelectYtbl_ProgCorte()
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROGCORTE order by id desc";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            
            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            List<Ytbl_ProgCorteModels> cortesProgramados = new List<Ytbl_ProgCorteModels>();
            try
            {
                if (myReader.HasRows)
                { 
                    while(myReader.Read())
                    {
                        Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();
                        RecordCount++;
                        
                        ProgCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        ProgCorte.Document_Name = myReader.GetString(1).ToString();
                        ProgCorte.Customer_Number_Upload = myReader.GetString(2).ToString();
                        ProgCorte.Nick_User = myReader.GetString(3).ToString();
                        ProgCorte.Date_Programed = DateTime.Parse(myReader.GetDateTime(4).ToString());
                        ProgCorte.Date_Upload = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        ProgCorte.IsValid = myReader.GetString(6).ToString();

                        cortesProgramados.Add(ProgCorte);
                    }
                }

                return cortesProgramados;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos||" + ex.ToString());
                return cortesProgramados;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public Ytbl_ProgCorteModels SelectYtbl_ProgCorteId(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select * from YTBL_PROGCORTE where id ="+id;

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int RecordCount = 0;
            Ytbl_ProgCorteModels ProgCorte = new Ytbl_ProgCorteModels();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        
                        RecordCount++;

                        ProgCorte.Id = int.Parse(myReader.GetDecimal(0).ToString());
                        ProgCorte.Document_Name = myReader.GetString(1).ToString();
                        ProgCorte.Customer_Number_Upload = myReader.GetString(2).ToString();
                        ProgCorte.Nick_User = myReader.GetString(3).ToString();
                        ProgCorte.Date_Programed = DateTime.Parse(myReader.GetDateTime(4).ToString());
                        ProgCorte.Date_Upload = DateTime.Parse(myReader.GetDateTime(5).ToString());
                        ProgCorte.IsValid = myReader.GetString(6).ToString();

                    }
                }

                return ProgCorte;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return ProgCorte;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public int SelectMaxId(string nameDocument)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            string commText = "select max(id) from YTBL_PROGCORTE where DOCUMENT_NAME = '"+ nameDocument+"'";

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            int id;
            
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        id = int.Parse(myReader.GetDecimal(0).ToString());
                        return id;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return -1;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }

        public List<Estadisticas> getStadistic(int id, string parameter)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();
            string commText = "";
            switch (parameter)
            {
                case "BANCO":
                    commText = "select count(*), banco from (select distinct(CPARTYACCOUNT_ID), banco from YTBL_DETALLEPROGCORTE where ID_PROGCORTE = "+id+") group by banco";
                    break;
                case "CIUDAD":
                    commText = "select count(*), CIUDAD from (select distinct(CPARTYACCOUNT_ID), CIUDAD from YTBL_DETALLEPROGCORTE where ID_PROGCORTE = "+id+") GROUP BY CIUDAD";
                    break;
                case "BUSINESS":
                    commText = "select count(*), BUSINESS from (select distinct(CPARTYACCOUNT_ID), BUSINESS from YTBL_DETALLEPROGCORTE where ID_PROGCORTE = "+id+") GROUP BY BUSINESS";
                    break;
                case "COMPANY":
                    commText = "select count(*), COMPANY from (select distinct(CPARTYACCOUNT_ID), COMPANY from YTBL_DETALLEPROGCORTE where ID_PROGCORTE = " + id + " ) GROUP BY COMPANY";
                    break;
                case "CUENTAS":
                    commText = "select COUNT(distinct CPARTYACCOUNT_ID), 'Cuentas' from YTBL_DETALLEPROGCORTE where ID_PROGCORTE = " + id;
                    break;
            }

            objConn.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = objConn;
            cmd.CommandText = commText;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader myReader = cmd.ExecuteReader();

            List<Estadisticas> ListEstadictica = new List<Estadisticas>();

            Ytbl_CondicionesCorte condicion = new Ytbl_CondicionesCorte();
            try
            {
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        Estadisticas estadistica = new Estadisticas();

                        try
                        {
                            if(parameter == "CIUDAD")
                            {
                                estadistica.cantidad = int.Parse(myReader.GetDecimal(0).ToString());
                                if (myReader.GetValue(1).ToString() != "")
                                    estadistica.nombre = condicion.getNameProperty(myReader.GetValue(1).ToString(), "CIUDAD");
                                else
                                    estadistica.nombre = "Ninguno";
                            }
                            else
                            {
                                estadistica.cantidad = int.Parse(myReader.GetDecimal(0).ToString());
                                if (myReader.GetValue(1).ToString() != "")
                                    estadistica.nombre = myReader.GetValue(1).ToString();
                                else
                                    estadistica.nombre = "Ninguno";
                            }   
                        }
                        catch (Exception ex)
                        {
                            estadistica.nombre = "Ninguno";
                        }

                        ListEstadictica.Add(estadistica);
                    }
                }

                return ListEstadictica;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return ListEstadictica;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }

        }

        public List<ContextReport> getReport(int id)
        {
            conn = new Connection();
            OleDbConnection objConn = conn.Conn();

            List<ContextReport> listReport = new List<ContextReport>();

            string commText = "select a.cparty_id, a.cpartyaccount_id, a.ID_PROGCORTE, b.ID_BLOQUE, b.CONT_BLOQUE, a.status, b.EJE_DATE"
                            + " from YTBL_DETALLEPROGCORTE a, YTBL_CORTESCLIENTES b where a.ID_PROGCORTE = b.ID_CORTE and a.ID_PROGCORTE  = '" + id + "'";

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
                        ContextReport report = new ContextReport();

                        report.cpartyId = Int64.Parse(myReader.GetDecimal(0).ToString());
                        report.cpartyAccountId = Int64.Parse(myReader.GetDecimal(1).ToString());
                        report.idProgCorte = int.Parse(myReader.GetDecimal(2).ToString());
                        report.IdBloque = int.Parse(myReader.GetDecimal(3).ToString());
                        report.ContBloque = int.Parse(myReader.GetDecimal(4).ToString());
                        report.status = myReader.GetString(5).ToString();
                        report.ejeDate = DateTime.Parse(myReader.GetDateTime(6).ToString());

                        listReport.Add(report);
                    }
                }

                return listReport;
            }
            catch (Exception ex)
            {
                myReader.Close();
                objConn.Close();
                Logs.WriteErrorLog("Error en la consulta de datos por ID||" + ex.ToString());
                return listReport;
            }
            finally
            {
                myReader.Close();
                objConn.Close();
            }
        }
    }

    public class ContextReport
    {
        [Display(Name = "Cparyid")]
        public Int64? cpartyId { get; set; }

        [Display(Name = "Accountid")]
        public Int64? cpartyAccountId { get; set; }

        [Display(Name = "Id corte")]
        public int? idProgCorte { get; set; }

        [Display(Name = "Id bloque")]
        public int? IdBloque { get; set; }

        [Display(Name = "Contador bloque")]
        public int? ContBloque { get; set; }

        [Display(Name = "Estado")]
        public string status { get; set; }

        [Display(Name = "Fecha ejecución")]
        public DateTime? ejeDate { get; set; }

    }
}