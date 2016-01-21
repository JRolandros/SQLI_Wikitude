using System;
using NUnit.Framework;
using SQLI_CrossAR.CrossAR.DataAccess.DB;
using SQLI_CrossAR.CrossAR.Models;
using System.Collections.Generic;
using SQLI_CrossAR.CrossAR.DataAccess.DAO;

namespace UnitTestApp
{
    [TestFixture]
    public class TestsSample
    {
        DBImpl _dbi;
        DAOPointInteret _dao;

        [SetUp]
        public void Setup()
        {
            _dbi = DBImpl.GetDBImpl();
            _dao = DAOPointInteret.GetDAOImplInstance();
        }

        #region TestDataAcess
        
        //OK
        [Test]
        public void TestDBCreateDB()
        {
            bool result = false;
            result = _dbi.CreateDBIfNotExist();
            Assert.True(result.Equals(true));
        }

        //OK
        [Test]
        public void TestDBCreateTable()
        {
            bool result = false;
            result = _dbi.CreateTablePIIfNotExist();
            Assert.True(result.Equals(true));
        }

        //OK
        [Test]
        public void TestDBInsertPI()
        {
            bool result = false;
            var PI = new PointInteret
            {
                ImageSource ="Icon.png",
                Nom ="PI test",
                Adresse ="1 rue du test",
                Latitude =0.0001,
                Longitude =0.0001,
                Categorie ="Test"
            };
            result = _dbi.InsertPI(PI);
            Assert.True(result.Equals(true));

        }

        //KO
        [Test]
        public void TestGetPITable()
        {            
            bool result = false;
            var PI1 = new PointInteret
            {
                ImageSource = "Icon.png",
                Nom = "PI test 1",
                Adresse = "1 rue du test",
                Latitude = 0.0001,
                Longitude = 0.0001,
                Categorie = "Test"
            };
            _dbi.InsertPI(PI1);

            var PI2 = new PointInteret
            {
                ImageSource = "Icon.png",
                Nom = "PI test 2",
                Adresse = "2 rue du test",
                Latitude = 0.0001,
                Longitude = 0.0001,
                Categorie = "Test"
            };
            _dbi.InsertPI(PI2);

            List<PointInteret> list = _dbi.GetPITable();
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestDBReadPITable()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestDBGetPIById()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestDBDeletePIById()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestDAOInitDB()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestDAOFormatQuery()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }
        #endregion TestDataAcess

        #region TestAlgorithm
        [Test]
        public void TestDeg2Rad()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestRad2Deg()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestComputeDistanceBetweenTwoPoints()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestAngleBetweenDeviceOrientationAndPI()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }

        [Test]
        public void TestGetPointingOrientationInDeg()
        {
            bool result = false;
            Assert.True(result.Equals(true));
        }
        #endregion TestAlgorithm

    }
}