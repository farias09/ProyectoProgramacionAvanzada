using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class HistorialDePedidos
    {
        [Key]
        public int id_historialPedido { get; set; }
        public int id_usuario { get; set; }
        public int id_carritoCompra { get; set; }

        [ForeignKey("id_usuario")]
        public virtual Usuarios Usuario { get; set; }

        [ForeignKey("id_carritoCompra")]
        public virtual CarritoDeCompras CarritoCompra { get; set; }
    }
}