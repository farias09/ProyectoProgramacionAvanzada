namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToRegistroCompra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RegistroCompras", "Usuarios_id_usuario", "dbo.Usuarios");
            DropIndex("dbo.RegistroCompras", new[] { "Usuarios_id_usuario" });
            RenameColumn(table: "dbo.RegistroCompras", name: "Usuarios_id_usuario", newName: "UsuarioId");
            AlterColumn("dbo.RegistroCompras", "UsuarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.RegistroCompras", "UsuarioId");
            AddForeignKey("dbo.RegistroCompras", "UsuarioId", "dbo.Usuarios", "id_usuario", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistroCompras", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.RegistroCompras", new[] { "UsuarioId" });
            AlterColumn("dbo.RegistroCompras", "UsuarioId", c => c.Int());
            RenameColumn(table: "dbo.RegistroCompras", name: "UsuarioId", newName: "Usuarios_id_usuario");
            CreateIndex("dbo.RegistroCompras", "Usuarios_id_usuario");
            AddForeignKey("dbo.RegistroCompras", "Usuarios_id_usuario", "dbo.Usuarios", "id_usuario");
        }
    }
}
