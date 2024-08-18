namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarRegistroCompra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistroCompras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaCompra = c.DateTime(nullable: false),
                        MontoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegistroCompras");
        }
    }
}
