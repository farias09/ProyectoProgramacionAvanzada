namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistorialDePedidos", "id_usuario", "dbo.Usuarios");
            AddForeignKey("dbo.HistorialDePedidos", "id_usuario", "dbo.Usuarios", "id_usuario", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistorialDePedidos", "id_usuario", "dbo.Usuarios");
            AddForeignKey("dbo.HistorialDePedidos", "id_usuario", "dbo.Usuarios", "id_usuario");
        }
    }
}
