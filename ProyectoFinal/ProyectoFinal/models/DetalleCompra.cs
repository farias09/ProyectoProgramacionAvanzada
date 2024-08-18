using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }
        public int RegistroCompraId { get; set; } 
        public int ProductoId { get; set; } 
        public int Cantidad { get; set; } 

        [ForeignKey("RegistroCompraId")]
        public virtual RegistroCompra RegistroCompra { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Productos Producto { get; set; }
    }
}