namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoDeCompras",
                c => new
                    {
                        id_carritoCompra = c.Int(nullable: false, identity: true),
                        cantidad = c.Int(nullable: false),
                        montoTotal = c.Single(nullable: false),
                        fechaCompra = c.DateTime(nullable: false),
                        id_usuario = c.Int(nullable: false),
                        id_producto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_carritoCompra)
                .ForeignKey("dbo.Productos", t => t.id_producto, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_usuario)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        id_producto = c.Int(nullable: false, identity: true),
                        codigoProducto = c.String(),
                        nombreProducto = c.String(),
                        descripcion = c.String(),
                        precioProducto = c.Single(nullable: false),
                        disponibilidadInventario = c.Int(nullable: false),
                        imagenAlmacenada = c.String(),
                        estadoProducto = c.Boolean(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_producto)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reseñas",
                c => new
                    {
                        id_reseña = c.Int(nullable: false, identity: true),
                        fechaPublicacion = c.DateTime(nullable: false),
                        descripcion = c.String(),
                        id_usuario = c.Int(nullable: false),
                        id_producto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_reseña)
                .ForeignKey("dbo.Productos", t => t.id_producto)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_usuario)
                .Index(t => t.id_producto);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id_usuario = c.Int(nullable: false, identity: true),
                        codigoUsuario = c.String(),
                        nombreUsuario = c.String(),
                        password = c.String(),
                        ultimaFechaConexion = c.DateTime(nullable: false),
                        estadoUsuario = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_usuario);
            
            CreateTable(
                "dbo.HistorialDePedidos",
                c => new
                    {
                        id_historialPedido = c.Int(nullable: false, identity: true),
                        id_usuario = c.Int(nullable: false),
                        id_carritoCompra = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_historialPedido)
                .ForeignKey("dbo.CarritoDeCompras", t => t.id_carritoCompra)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_usuario)
                .Index(t => t.id_carritoCompra);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id_rol = c.Int(nullable: false, identity: true),
                        nombreRol = c.String(),
                        id_usuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_rol)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario, cascadeDelete: true)
                .Index(t => t.id_usuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoDeCompras", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.CarritoDeCompras", "id_producto", "dbo.Productos");
            DropForeignKey("dbo.Reseñas", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Roles", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.HistorialDePedidos", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.HistorialDePedidos", "id_carritoCompra", "dbo.CarritoDeCompras");
            DropForeignKey("dbo.Reseñas", "id_producto", "dbo.Productos");
            DropForeignKey("dbo.Productos", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Roles", new[] { "id_usuario" });
            DropIndex("dbo.HistorialDePedidos", new[] { "id_carritoCompra" });
            DropIndex("dbo.HistorialDePedidos", new[] { "id_usuario" });
            DropIndex("dbo.Reseñas", new[] { "id_producto" });
            DropIndex("dbo.Reseñas", new[] { "id_usuario" });
            DropIndex("dbo.Productos", new[] { "CategoriaId" });
            DropIndex("dbo.CarritoDeCompras", new[] { "id_producto" });
            DropIndex("dbo.CarritoDeCompras", new[] { "id_usuario" });
            DropTable("dbo.Roles");
            DropTable("dbo.HistorialDePedidos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Reseñas");
            DropTable("dbo.Categorias");
            DropTable("dbo.Productos");
            DropTable("dbo.CarritoDeCompras");
        }
    }
}
