namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarDetalleCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleCompras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistroCompraId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.RegistroCompras", t => t.RegistroCompraId, cascadeDelete: true)
                .Index(t => t.RegistroCompraId)
                .Index(t => t.ProductoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleCompras", "RegistroCompraId", "dbo.RegistroCompras");
            DropForeignKey("dbo.DetalleCompras", "ProductoId", "dbo.Productos");
            DropIndex("dbo.DetalleCompras", new[] { "ProductoId" });
            DropIndex("dbo.DetalleCompras", new[] { "RegistroCompraId" });
            DropTable("dbo.DetalleCompras");
        }
    }
}
