using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeManager.BL.Entities;
using TimeManager.Common.Entities;
using TimeManager.Common.Enums;

namespace TimeManager.Testing.BL.Entities
{
    [TestClass]
    public class CatalogoBL_Test
    {
        [TestMethod]
        public void CatalogoBL_Departamento_Create()
        {
            Catalogo _Catalogo = new Catalogo()
            {
                Id = 0,
                EsActivo = false,
                FechaRegistro = DateTime.Now,                
                Nombre = "Prueba",
            };

            var Catalogo = CatalogoBL.Create(_Catalogo, CatalogoEnum.Departamento);

            Assert.IsTrue(Catalogo.Id > 0);
        }

        [TestMethod]
        public void CatalogoBL_EstadoVisita_Create()
        {
            Catalogo _Catalogo = new Catalogo()
            {
                Id = 0,
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Nombre = "Prueba",
            };

            var Catalogo = CatalogoBL.Create(_Catalogo, CatalogoEnum.EstadoVisita);

            Assert.IsTrue(Catalogo.Id > 0);
        }

        [TestMethod]
        public void CatalogoBL_TiempoInvertido_Create()
        {
            Catalogo _Catalogo = new Catalogo()
            {
                Id = 0,
                EsActivo = false,
                FechaRegistro = DateTime.Now,
                Nombre = "Prueba",
            };

            var Catalogo = CatalogoBL.Create(_Catalogo, CatalogoEnum.TiempoInvertido);

            Assert.IsTrue(Catalogo.Id > 0);
        }

        [TestMethod]
        public void CatalogoBL_Departamento_GetList()
        {
            var ListCatalogo = CatalogoBL.GetList(CatalogoEnum.Departamento, false);

            Assert.IsNotNull(ListCatalogo);
        }

        [TestMethod]
        public void CatalogoBL_Departamento_GetById()
        {
            var ListCatalogo = CatalogoBL.GetList(CatalogoEnum.Departamento, false);

            var Catalogo = CatalogoBL.GetById(ListCatalogo.First().Id, CatalogoEnum.Departamento, false);

            Assert.IsNotNull(Catalogo);
        }

        [TestMethod]
        public void CatalogoBL_Departamento_Update()
        {
            var ListCatalogo = CatalogoBL.GetList(CatalogoEnum.Departamento, false);

            var _Catalogo = ListCatalogo.First();
            _Catalogo.Nombre = "Prueba unitaria modificada";

            bool Result = CatalogoBL.Update(_Catalogo, CatalogoEnum.Departamento);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void CatalogoBL_Departamento_Delete()
        {
            var ListCatalogo = CatalogoBL.GetList(CatalogoEnum.Departamento, false);

            var _Catalogo = ListCatalogo.First();

            bool Result = CatalogoBL.Delete(_Catalogo.Id, CatalogoEnum.Departamento);

            Assert.IsTrue(Result);
        }
    }
}
