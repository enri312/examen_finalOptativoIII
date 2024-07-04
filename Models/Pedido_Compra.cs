using System;
using System.Collections.Generic;

namespace SuperProjectDapperFinal.Models
{
    public class Pedido_Compra
    {
        public int id_pedido_compra { get; set; } // Primary Key
        public int id_proveedor { get; set; } // Foreign Key
        public int id_sucursal { get; set; } // Foreign Key
        public DateTime fecha_hora { get; set; }
        public decimal total { get; set; }

        // Navigation properties
        public Proveedor Proveedor { get; set; }
        public Sucursal Sucursal { get; set; }

        // Agrega esta propiedad
        public List<Detalle_Pedido> Detalles { get; set; }
    }
}
