using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class CarritoDeCompras
    {
        [Key]
        public int id_carritoCompra { get; set; }
        public int cantidad { get; set; }
        public float montoTotal { get; set; }
        public DateTime fechaCompra { get; set; }
        public int id_usuario { get; set; }
        public int id_producto { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuarios Usuario { get; set; }

        [ForeignKey("id_producto")]
        public virtual Productos Producto { get; set; }
    }
}