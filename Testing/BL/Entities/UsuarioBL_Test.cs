using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class UsuarioBL_Test
    {
        [TestMethod]
        public void UsuarioBL_Create()
        {
            Usuario _Usuario = new Usuario()
            {
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Id = 0,
                Apellido = "Unitaria",
                Nombre = "Prueba",
                Contrasenia = "******",
                Correo = "prueba_unitaria@prueba.test"
            };

            var Usuario = UsuarioBL.Create(_Usuario);

            Assert.IsTrue(Usuario.Id > 0);
        }

        [TestMethod]
        public void UsuarioBL_GetList()
        {
            var ListUsuario = UsuarioBL.GetList(false);

            Assert.IsNotNull(ListUsuario);
        }

        [TestMethod]
        public void UsuarioBL_GetById()
        {
            var ListUsuario = UsuarioBL.GetList(false);

            var Usuario = UsuarioBL.GetById(ListUsuario.First().Id, false);

            Assert.IsNotNull(Usuario);
        }

        [TestMethod]
        public void UsuarioBL_Update()
        {
            var ListUsuario = UsuarioBL.GetList(false);

            var _Usuario = ListUsuario.First();
            _Usuario.Nombre = "Prueba unitaria modificada";

            bool Result = UsuarioBL.Update(_Usuario);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void UsuarioBL_Delete()
        {
            var ListUsuario = UsuarioBL.GetList(false);

            var _Usuario = ListUsuario.First();

            bool Result = UsuarioBL.Delete(_Usuario.Id);

            Assert.IsTrue(Result);
        }
    }
}
