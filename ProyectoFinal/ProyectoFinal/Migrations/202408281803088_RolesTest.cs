namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "id_usuario", "dbo.Usuarios");
            DropIndex("dbo.Roles", new[] { "id_usuario" });
            AddColumn("dbo.Usuarios", "ID_Rol", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "ID_Rol");
            CreateIndex("dbo.Roles", "id_usuario");
            AddForeignKey("dbo.Roles", "id_usuario", "dbo.Usuarios", "id_usuario", cascadeDelete: true);
        }
    }
}
