using SuperProjectDapperFinal.Models;
using System;

namespace SuperProjectDapperFinal.Models
{
    public class Detalle_Pedido
    {
        public int id_detalle_pedido { get; set; } // Primary Key
        public int id_pedido_compra { get; set; } // Foreign Key
        public int id_producto { get; set; } // Foreign Key
        public int cantidad_producto { get; set; }
        public decimal subtotal { get; set; }

        // Navigation properties
        public Pedido_Compra Pedido_Compra { get; set; }
        public Productos Producto { get; set; }
    } //rpueba
}

