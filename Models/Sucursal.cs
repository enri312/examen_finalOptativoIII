using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperProjectDapperFinal.Models
{
    public class Sucursal
    {
        public int id_sucursal { get; set; }// Primary key
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string whatsapp { get; set; }
        public string mail { get; set; }
        public bool estado { get; set; }
    }
}

