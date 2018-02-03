using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TimeManager.Common.Entities
{ 
    public class Catalogo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EsActivo { get; set; }

        public static SelectList GetSelectListFromCatalog(List<Catalogo> list, bool hasSelectedItem = true, string textSelectedItem = "Seleccione")
        {
            List<SelectListItem> ListItems = list.Select(x => new SelectListItem() { Text = x.Nombre, Value = x.Id.ToString() }).ToList();

            if (hasSelectedItem)
            {
                ListItems.Insert(0, new SelectListItem() { Text = textSelectedItem, Value = "" });
            }

            return new SelectList(ListItems, "Value", "Text"); ;
        }
    }
}
