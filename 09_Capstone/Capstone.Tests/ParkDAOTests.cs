using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkDAOTests : DAOTests
    {
        [TestMethod]
        public void ParkListPopulatesWith2Parks()
        {
            IParkSqlDAO dao = new ParkSqlDAO(connectionString);

            IList<Park> parkList = dao.GetAllParks();

            int parkCount = parkList.Count;


            Assert.AreEqual(2, parkCount);
        }


    }
}
