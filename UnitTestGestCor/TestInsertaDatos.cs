using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestCor.Models;
using System.Collections.Generic;
using GestCor.Clases;

namespace UnitTestGestCor
{
    [TestClass]
    public class TestInsertaDatos
    {
        [TestMethod]
        public void TestGuardaProgCorte()
        {
            Ytbl_ProgCorteModels save = new Ytbl_ProgCorteModels();

            save.Document_Name = "Prueba1";
            save.Customer_Number_Upload = "1";
            save.Nick_User = "jyandun";
            save.Date_Programed = DateTime.Now;
            save.Date_Upload = DateTime.Now;
            save.IsValid = "Y";


            //save.SaveYtbl_ProgCorte();
        }

        [TestMethod]
        public void TestGuardaDetalleCorte()
        {
            Ytbl_DetalleProgCorteModels save = new Ytbl_DetalleProgCorteModels();

            save.id_ProgCorte = 1;
            save.CpartyId = 5546992;
            save.CpartyAccountId = 59464368;
            save.CitemId = 110577969;
            save.Ciudad = 8;
            save.BancoId = "BANCO PACIFICO";
            save.FormaPago = "Debito Bancario corriente";
            save.EmpresaFacturadora = "Setel";
            save.TipoNegocio = "Combo Int - Telefonia";
            save.FieldV1 = null;
            save.FieldV2 = null;
            save.FieldN1 = null;
            save.FieldN2 = null;
            save.FieldD1 = null;
            save.Status = "Q";

            //save.SaveYtbl_DetalleProgCorte();
        }

        [TestMethod]
        public void TestConsultaCortes()
        {
            Ytbl_ProgCorteModels progCorte = new Ytbl_ProgCorteModels();
            List<Ytbl_ProgCorteModels> listProgCorte = new List<Ytbl_ProgCorteModels>();

            List<Ytbl_ProgCorteModels> comparaLista = new List<Ytbl_ProgCorteModels>();

            Ytbl_ProgCorteModels save = new Ytbl_ProgCorteModels();

            save.Document_Name = "Prueba1";
            save.Customer_Number_Upload = "1";
            save.Nick_User = "jyandun";
            save.Date_Programed = DateTime.Now;
            save.Date_Upload = DateTime.Now;
            save.IsValid = "Y";

            comparaLista.Add(save);

            listProgCorte = progCorte.SelectYtbl_ProgCorte();

            Assert.AreEqual(comparaLista.Count, listProgCorte.Count);
        }

        [TestMethod]
        public void TestValidaStatus()
        {
            Connection con = new Connection();

            bool status = con.getStatusInstance();
        }
    }
}
