using Microsoft.VisualStudio.TestTools.UnitTesting;
using ais_client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ais_client.Tests
{
    [TestClass()]
    public class StocksTests
    {
        [TestMethod()]
        [DataRow("limit","buy","11","1",true)]
        [DataRow("","","1","1",false)]
        [DataRow("", "", "11", "1", false)]
        [DataRow("limit", "", "", "41", false)]
        [DataRow("market", "buy", "-11", "", false)]
        [DataRow("market", "sell", "", "33", true)]
        [DataRow("limit", "sell", "-14", "0", false)]
        public void CheckTextTest(string type, string func, string price, string count, bool result)
        {
            Assert.AreEqual(Stocks.CheckText(type,func,price,count),result);
        }
    }
}