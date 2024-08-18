using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
// System.Data es para utilizar el db context sobre la interfaz
using System.Data.Entity;

namespace ProyectoFinal.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Reseñas> Resenas { get; set; }
        public DbSet<CarritoDeCompras> CarritoDeCompras { get; set; }
        public DbSet<HistorialDePedidos> HistorialDePedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<RegistroCompra> RegistroCompras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>()
                .HasRequired(r => r.Usuario)
                .WithMany(u => u.Roles)
                .HasForeignKey(r => r.id_usuario)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Reseñas>()
                .HasRequired(r => r.Usuario)
                .WithMany(u => u.Reseñas)
                .HasForeignKey(r => r.id_usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reseñas>()
                .HasRequired(r => r.Producto)
                .WithMany(p => p.Reseña)
                .HasForeignKey(r => r.id_producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CarritoDeCompras>()
                .HasRequired(c => c.Usuario)
                .WithMany(u => u.CarritoDeCompras)
                .HasForeignKey(c => c.id_usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistorialDePedidos>()
                .HasRequired(h => h.Usuario)
                .WithMany(u => u.HistorialDePedidos)
                .HasForeignKey(h => h.id_usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistorialDePedidos>()
                .HasRequired(h => h.CarritoCompra)
                .WithMany()
                .HasForeignKey(h => h.id_carritoCompra)
                .WillCascadeOnDelete(false);
        }
    }
}