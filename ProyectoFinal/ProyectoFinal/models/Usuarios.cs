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

        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<Reseñas> Reseñas { get; set; }
        public virtual ICollection<CarritoDeCompras> CarritoDeCompras { get; set; }
        public virtual ICollection<HistorialDePedidos> HistorialDePedidos { get; set; }

        public Usuarios()
        {
            Roles = new HashSet<Roles>();
            Reseñas = new HashSet<Reseñas>();
            CarritoDeCompras = new HashSet<CarritoDeCompras>();
            HistorialDePedidos = new HashSet<HistorialDePedidos>();
        }
    }
}