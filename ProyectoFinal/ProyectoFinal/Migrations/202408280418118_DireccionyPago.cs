namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DireccionyPago : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "DireccionPrincipal", c => c.String());
            AddColumn("dbo.Usuarios", "MetodoDePago", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "MetodoDePago");
            DropColumn("dbo.Usuarios", "DireccionPrincipal");
        }
    }
}
