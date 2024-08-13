namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarTituloModelReseñas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reseñas", "titulo", c => c.String(nullable: false));
            AlterColumn("dbo.Reseñas", "descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reseñas", "descripcion", c => c.String());
            DropColumn("dbo.Reseñas", "titulo");
        }
    }
}
