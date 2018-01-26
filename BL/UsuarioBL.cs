using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Common.Transaction;
using TimeManager.DAL.Querys;
using TimeManager.Entities;

namespace TimeManager.BL
{
    public class UsuarioBL
    {
        //public static List<Usuario> GetData(Query Query)
        //{
        //    List<Usuario> Usuario = new List<Usuario>();
        //    try
        //    {
        //        DataTable Table = new DataTable();
        //        if (Query.IsResolve && Query.IsQuery)
        //        {
        //            Table = Query.Result.ResultQuery;
        //        }
        //        foreach (DataRow Row in Table.Rows)
        //        {
        //            Usuario.Add(new Persona()
        //            {
        //                Apellido = Row.IsNull("Apellido") ? "" : Row["Apellido"].ToString(),
        //                Correo = Row.IsNull("Correo") ? "" : Row["Correo"].ToString(),
        //                Edad = Row.IsNull("Edad") ? (short)0 : short.Parse(Row["Edad"].ToString()),
        //                Estado = Row.IsNull("Estado") ? true : bool.Parse(Row["Estado"].ToString()),
        //                Id = Row.IsNull("Id") ? 0 : long.Parse(Row["Id"].ToString()),
        //                Nombre = Row.IsNull("Nombre") ? "" : Row["Nombre"].ToString()
        //            });
        //        }

        //        //foreach (var item in Query.Includes)
        //        //{
        //        //    switch (item)
        //        //    {
        //        //        case "Persona.Propietario":
        //        //            var ListPropietarios = PropietarioBL.GetData(Query.FindFirst(Query.SubQuery, item));
        //        //            ListPersonas = ListPersonas.Select(x => new Persona()
        //        //            {
        //        //                Propietario = ListPropietarios.Where(p => p.PersonaId == x.Id),
        //        //                Apellido = x.Apellido,
        //        //                Correo = x.Correo,
        //        //                Edad = x.Edad,
        //        //                Estado = x.Estado,
        //        //                Id = x.Id,
        //        //                Nombre = x.Nombre
        //        //            }).ToList();
        //        //            break;
        //        //        default:
        //        //            break;
        //        //    }
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        // Exceptios
        //    }
        //    return ListUsuario;
        //}

        public static Query GetById(int id) {
            return UsuarioQuery.GetById(id);
        }

        public static Query Create(Usuario usuario)
        {
            return UsuarioQuery.Create(usuario);
        }
    }
}
