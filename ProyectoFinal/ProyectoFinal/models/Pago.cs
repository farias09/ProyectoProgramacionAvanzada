using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Pago
    {
        public List<ItemDeCarrito> Items { get; set; }

        [Key]
        public int PagoId { get; set; }

        public float MontoTotal { get; set; }
        public string MetodoDePago { get; set; }
        public string NumeroTarjeta { get; set; }
        public string NumeroSinpe { get; set; }
        public float MontoEfectivo { get; set; }
    }
}