using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperProjectDapperFinal.Models
{
    public class Proveedor
    {
        public int id_proveedor { get; set; } // Primary Key
        public string razon_social { get; set; }
        public string tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string direccion { get; set; }
        public string mail { get; set; }
        public string celular { get; set; }
        public bool estado { get; set; }
    }
}