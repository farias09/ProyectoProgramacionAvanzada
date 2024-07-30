namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Compras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        PagoId = c.Int(nullable: false, identity: true),
                        MontoTotal = c.Single(nullable: false),
                        MetodoDePago = c.String(),
                        NumeroTarjeta = c.String(),
                        NumeroSinpe = c.String(),
                        MontoEfectivo = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PagoId);
            
            AddColumn("dbo.ItemDeCarritoes", "Pago_PagoId", c => c.Int());
            CreateIndex("dbo.ItemDeCarritoes", "Pago_PagoId");
            AddForeignKey("dbo.ItemDeCarritoes", "Pago_PagoId", "dbo.Pagoes", "PagoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemDeCarritoes", "Pago_PagoId", "dbo.Pagoes");
            DropIndex("dbo.ItemDeCarritoes", new[] { "Pago_PagoId" });
            DropColumn("dbo.ItemDeCarritoes", "Pago_PagoId");
            DropTable("dbo.Pagoes");
        }
    }
}
