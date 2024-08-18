namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizarModelo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarritoDeCompras", "montoTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarritoDeCompras", "montoTotal", c => c.Single(nullable: false));
        }
    }
}
