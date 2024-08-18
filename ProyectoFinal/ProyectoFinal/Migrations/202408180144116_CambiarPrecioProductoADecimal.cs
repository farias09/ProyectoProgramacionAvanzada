namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiarPrecioProductoADecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productos", "precioProducto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productos", "precioProducto", c => c.Single(nullable: false));
        }
    }
}
