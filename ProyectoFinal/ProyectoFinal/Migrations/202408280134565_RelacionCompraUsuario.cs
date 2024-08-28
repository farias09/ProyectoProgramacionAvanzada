namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionCompraUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistroCompras", "Usuarios_id_usuario", c => c.Int());
            CreateIndex("dbo.RegistroCompras", "Usuarios_id_usuario");
            AddForeignKey("dbo.RegistroCompras", "Usuarios_id_usuario", "dbo.Usuarios", "id_usuario");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistroCompras", "Usuarios_id_usuario", "dbo.Usuarios");
            DropIndex("dbo.RegistroCompras", new[] { "Usuarios_id_usuario" });
            DropColumn("dbo.RegistroCompras", "Usuarios_id_usuario");
        }
    }
}
