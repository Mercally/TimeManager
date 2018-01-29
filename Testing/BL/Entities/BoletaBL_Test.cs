using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;
using TimeManager.Common.Enums;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class BoletaBL_Test
    {
        [TestMethod]
        public void BoletaBL_Create()
        {
            var LisCliente = ClienteBL.GetList(false);
            var ListUsuario = UsuarioBL.GetList(false);
            var ListProyecto = ProyectoBL.GetList(false);
            var ListDepartamento = CatalogoBL.GetList(CatalogoEnum.Departamento, false);
            var ListTiempoInvertido = CatalogoBL.GetList(CatalogoEnum.TiempoInvertido, false);

            Boleta _Boleta = new Boleta()
            {
                Id = 0,
                ClienteId = LisCliente.First().Id,
                DepartamentoId = ListDepartamento.First().Id,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now,
                HoraEntrada = DateTime.Now.TimeOfDay,
                HoraSalida = DateTime.Now.TimeOfDay,
                NumeroBoleta = "PRUEBA",
                ProyectoId = ListProyecto.First().Id,
                TiempoEfectivo = 0,
                TiempoInvertidoEn = ListTiempoInvertido.First().Id,
                UsuarioId = ListUsuario.First().Id,
                EsActivo = false,
                FechaRegistro = DateTime.Now,
            };

            var Boleta = BoletaBL.Create(_Boleta);

            Assert.IsTrue(Boleta.Id > 0);
        }

        [TestMethod]
        public void BoletaBL_GetList()
        {
            var ListBoleta = BoletaBL.GetList(false);

            Assert.IsNotNull(ListBoleta);
        }

        [TestMethod]
        public void BoletaBL_GetById()
        {
            var ListBoleta = BoletaBL.GetList(false);

            var Boleta = BoletaBL.GetById(ListBoleta.First().Id, false);

            Assert.IsNotNull(Boleta);
        }

        [TestMethod]
        public void BoletaBL_Update()
        {
            var ListBoleta = BoletaBL.GetList(false);

            var _Boleta = ListBoleta.First();
            _Boleta.NumeroBoleta = "PRUEBA MODIFICADA";

            bool Result = BoletaBL.Update(_Boleta);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void BoletaBL_Delete()
        {
            var ListBoleta = BoletaBL.GetList(false);

            var _Boleta = ListBoleta.First();

            bool Result = BoletaBL.Delete(_Boleta.Id);

            Assert.IsTrue(Result);
        }
    }
}
