using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Categorias
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Productos> Productos { get; set; }
    }
}