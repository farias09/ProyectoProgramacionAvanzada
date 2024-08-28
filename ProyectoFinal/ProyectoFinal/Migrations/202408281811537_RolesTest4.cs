namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesTest4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Usuarios", "ID_Rol");
            AddForeignKey("dbo.Usuarios", "ID_Rol", "dbo.Roles", "id_rol", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "ID_Rol", "dbo.Roles");
            DropIndex("dbo.Usuarios", new[] { "ID_Rol" });
        }
    }
}
