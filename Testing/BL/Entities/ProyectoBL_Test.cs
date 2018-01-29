using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class ProyectoBL_Test
    {
        [TestMethod]
        public void ProyectoBL_Create()
        {
            Proyecto _Proyecto = new Proyecto()
            {
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Id = 0,
                Nombre = "Prueba unitaria"
            };

            var Proyecto = ProyectoBL.Create(_Proyecto);

            Assert.IsTrue(Proyecto.Id > 0);
        }

        [TestMethod]
        public void ProyectoBL_GetList()
        {
            var ListProyecto = ProyectoBL.GetList(false);

            Assert.IsNotNull(ListProyecto);
        }

        [TestMethod]
        public void ProyectoBL_GetById()
        {
            var ListProyecto = ProyectoBL.GetList(false);

            var Proyecto = ProyectoBL.GetById(ListProyecto.First().Id, false);

            Assert.IsNotNull(Proyecto);
        }

        [TestMethod]
        public void ProyectoBL_Update()
        {
            var ListProyecto = ProyectoBL.GetList(false);

            var _Proyecto = ListProyecto.First();
            _Proyecto.Nombre = "Prueba unitaria modificada";

            bool Result = ProyectoBL.Update(_Proyecto);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void ProyectoBL_Delete()
        {
            var ListProyecto = ProyectoBL.GetList(false);

            var _Proyecto = ListProyecto.First();

            bool Result = ProyectoBL.Delete(_Proyecto.Id);

            Assert.IsTrue(Result);
        }
    }
}
