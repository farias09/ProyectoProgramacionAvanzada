namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesTest2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "id_usuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "id_usuario", c => c.Int(nullable: false));
        }
    }
}
