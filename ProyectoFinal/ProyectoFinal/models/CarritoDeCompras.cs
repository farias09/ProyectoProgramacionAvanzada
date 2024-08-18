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
        public decimal montoTotal { get; set; }
        public DateTime fechaCompra { get; set; }
        public int id_usuario { get; set; }
        public int id_producto { get; set; }


        [ForeignKey("id_usuario")]
        public virtual Usuarios Usuario { get; set; }

        [ForeignKey("id_producto")]
        public virtual Productos Producto { get; set; }

        public List<ItemDeCarrito> Items { get; set; } = new List<ItemDeCarrito>();

        public void AgregarProducto(Productos producto) 
        {
            var item = Items.FirstOrDefault(i => i.Producto.id_producto == producto.id_producto);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                Items.Add(new ItemDeCarrito { Producto = producto, Cantidad = 1 });
            }

            montoTotal = Items.Sum(i => i.Cantidad * i.Producto.precioProducto);

        }

        public void EliminarProducto(int productoId)
        {
            var item = Items.FirstOrDefault(i => i.Producto.id_producto == productoId);
            if (item != null)
            {
                Items.Remove(item);
            }
            montoTotal = Items.Sum(i => i.Cantidad * i.Producto.precioProducto);
        }
    }
    
    public class ItemDeCarrito
    {
        public int Id { get; set; } // Clave primaria
        public Productos Producto { get; set; } 
        public int Cantidad { get; set; }
    }
}