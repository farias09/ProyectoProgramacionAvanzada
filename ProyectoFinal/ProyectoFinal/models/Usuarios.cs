using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public string codigoUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public DateTime ultimaFechaConexion { get; set; } = DateTime.Now;
        public bool estadoUsuario { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Biografia { get; set; }
        public string DireccionPrincipal { get; set; }
        public string MetodoDePago { get; set; }
        public int ID_Rol { get; set; } 

        [ForeignKey("ID_Rol")]
        public virtual Roles Rol { get; set; }

        public virtual ICollection<Reseñas> Reseñas { get; set; }
        public virtual ICollection<CarritoDeCompras> CarritoDeCompras { get; set; }
        public virtual ICollection<RegistroCompra> RegistroCompra { get; set; }

        public Usuarios()
        {
            Reseñas = new HashSet<Reseñas>();
            CarritoDeCompras = new HashSet<CarritoDeCompras>();
            RegistroCompra = new HashSet<RegistroCompra>();
        }
    }

}