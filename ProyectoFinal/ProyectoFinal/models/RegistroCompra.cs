using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class RegistroCompra
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal MontoTotal { get; set; }
        public virtual List<DetalleCompra> Detalles { get; set; }
    }
}