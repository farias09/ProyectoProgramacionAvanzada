namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeyToItemDeCarrito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemDeCarritoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Producto_id_producto = c.Int(),
                        CarritoDeCompras_id_carritoCompra = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productos", t => t.Producto_id_producto)
                .ForeignKey("dbo.CarritoDeCompras", t => t.CarritoDeCompras_id_carritoCompra)
                .Index(t => t.Producto_id_producto)
                .Index(t => t.CarritoDeCompras_id_carritoCompra);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemDeCarritoes", "CarritoDeCompras_id_carritoCompra", "dbo.CarritoDeCompras");
            DropForeignKey("dbo.ItemDeCarritoes", "Producto_id_producto", "dbo.Productos");
            DropIndex("dbo.ItemDeCarritoes", new[] { "CarritoDeCompras_id_carritoCompra" });
            DropIndex("dbo.ItemDeCarritoes", new[] { "Producto_id_producto" });
            DropTable("dbo.ItemDeCarritoes");
        }
    }
}
