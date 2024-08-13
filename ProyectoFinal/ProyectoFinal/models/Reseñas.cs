using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models
{
    public class Reseñas
    {
        [Key]
        public int id_reseña { get; set; }

        public int id_producto { get; set; }
        [ForeignKey("id_producto")]
        public virtual Productos Producto { get; set; }

        public int id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuarios Usuario { get; set; }

        [Required]
        public string titulo { get; set; }

        [Required]
        public string descripcion { get; set; }

        public DateTime fechaPublicacion { get; set; } = DateTime.Now;
    }
}
