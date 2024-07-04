using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperProjectDapperFinal.Models
{
    public class Productos
    {
        public int id_producto { get; set; } // Primary Key
        public string descripcion { get; set; }
        public int cantidad_minima { get; set; }
        public int cantidad_stock { get; set; }
        public int precio_compra { get; set; }
        public int precio_venta { get; set; }
        public string categoria { get; set; }
        public string marca { get; set; }
        public bool estado { get; set; }
    }
}
