using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Productos
    {
        [Key]
        public int id_producto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcion { get; set; }
        public float precioProducto { get; set; }
        public int disponibilidadInventario { get; set; }
        public string imagenAlmacenada { get; set; }
        public bool estadoProducto { get; set; }

        public virtual ICollection<Reseñas> Reseña { get; set; }

        public Productos()
        {
            Reseña = new HashSet<Reseñas>();
        }
    }
}