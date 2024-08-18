using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public decimal precioProducto { get; set; }
        public int disponibilidadInventario { get; set; }
        public string imagenAlmacenada { get; set; }
        public bool estadoProducto { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categorias Categoria { get; set; }

        public virtual ICollection<Reseñas> Reseña { get; set; }

        public Productos()
        {
            Reseña = new HashSet<Reseñas>();
        }
    }
}