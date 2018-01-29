using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.DAL.Connectivity;

namespace TimeManager.Testing.DAL.Connectivity
{
    [TestClass]
    public class Connection_Test
    {
        Connection con = new Connection();

        [TestMethod]
        public void Connection_Connect()
        {
            var Connection = con.Connect();

            Assert.IsNotNull(Connection);
        }
    }
}
