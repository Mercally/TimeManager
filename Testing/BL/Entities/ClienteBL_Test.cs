using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class ClienteBL_Test
    {
        [TestMethod]
        public void ClienteBL_Create()
        {
            Cliente _Cliente = new Cliente()
            {
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Id = 0,
                Nombre = "Prueba unitaria"
            };

            var Cliente = ClienteBL.Create(_Cliente);

            Assert.IsTrue(Cliente.Id > 0);
        }

        [TestMethod]
        public void ClienteBL_GetList()
        {
            var ListCliente = ClienteBL.GetList(false);

            Assert.IsNotNull(ListCliente);
        }

        [TestMethod]
        public void ClienteBL_GetById()
        {
            var ListCliente = ClienteBL.GetList(false);

            var Cliente = ClienteBL.GetById(ListCliente.First().Id, false);

            Assert.IsNotNull(Cliente);
        }

        [TestMethod]
        public void ClienteBL_Update()
        {
            var ListCliente = ClienteBL.GetList(false);

            var _Cliente = ListCliente.First();
            _Cliente.Nombre = "Prueba unitaria modificada";

            bool Result = ClienteBL.Update(_Cliente);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void ClienteBL_Delete()
        {
            var ListCliente = ClienteBL.GetList(false);

            var _Cliente = ListCliente.First();

            bool Result = ClienteBL.Delete(_Cliente.Id);

            Assert.IsTrue(Result);
        }
    }
}
