using Microsoft.VisualStudio.TestTools.UnitTesting;
using ais_client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ais_client.Tests
{
    [TestClass()]
    public class AuthTests
    {
        [TestMethod()]
        [DataRow("Ilei Cjdi Ofekd", "1943205432", true)]
        [DataRow("Ilei Cjdi Ofekd", "1943232",true)]
        [DataRow("Ilei Cjdi", "1943205432", false)]
        [DataRow("Ilei", "1943205432", false)]
        [DataRow("Vladimirov Vrysha Olegovich", "1944309361",true)]
        [DataRow("Vladimirov VryshaOlegovich", "19443!761", false)]
        [DataRow("VladimirovVrysha Olegovich", "FJ19449361", false)]
        public void Auth_clickTest(string FIO, string passport, bool result)
        {
           Assert.AreEqual(Auth.CheckData(FIO, passport), result);
        }
    }
}