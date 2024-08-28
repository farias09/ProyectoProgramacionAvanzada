namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerfilUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "FotoPerfilUrl", c => c.String());
            AddColumn("dbo.Usuarios", "Biografia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Biografia");
            DropColumn("dbo.Usuarios", "FotoPerfilUrl");
        }
    }
}
