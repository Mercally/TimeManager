using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;
using TimeManager.Common.Enums;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class ActividadBL_Test
    {
        [TestMethod]
        public void ActividadBL_Create()
        {
            var ListEstadoVisita = CatalogoBL.GetList(CatalogoEnum.EstadoVisita, false);
            Actividad _Actividad = new Actividad()
            {
                Id = 0,
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Descripcion = "Prueba unitaria",
                EstadoId = ListEstadoVisita.First().Id
            };

            var Actividad = ActividadBL.Create(_Actividad);

            Assert.IsTrue(Actividad.Id > 0);
        }

        [TestMethod]
        public void ActividadBL_GetList()
        {
            var ListActividad = ActividadBL.GetList(false);

            Assert.IsNotNull(ListActividad);
        }

        [TestMethod]
        public void ActividadBL_GetById()
        {
            var ListActividad = ActividadBL.GetList(false);

            var Actividad = ActividadBL.GetById(ListActividad.First().Id, false);

            Assert.IsNotNull(Actividad);
        }

        [TestMethod]
        public void ActividadBL_Update()
        {
            var ListActividad = ActividadBL.GetList(false);

            var _Actividad = ListActividad.First();
            _Actividad.Descripcion = "Prueba unitaria modificada";

            bool Result = ActividadBL.Update(_Actividad);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void ActividadBL_Delete()
        {
            var ListActividad = ActividadBL.GetList(false);

            var _Actividad = ListActividad.First();

            bool Result = ActividadBL.Delete(_Actividad.Id);

            Assert.IsTrue(Result);
        }
    }
}
