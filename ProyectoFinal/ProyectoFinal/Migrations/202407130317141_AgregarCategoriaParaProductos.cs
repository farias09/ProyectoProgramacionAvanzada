
namespace ProyectoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddCategoriaToProductos : DbMigration
    {
        public override void Up()
        {
            // Primero, crea la tabla Categorias
            CreateTable(
                "dbo.Categorias",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nombre = c.String(),
                })
                .PrimaryKey(t => t.Id);

            // Luego, añade la columna CategoriaId a Productos
            AddColumn("dbo.Productos", "CategoriaId", c => c.Int(nullable: false, defaultValue: 1));

            // Finalmente, crea la clave foránea
            CreateIndex("dbo.Productos", "CategoriaId");
            AddForeignKey("dbo.Productos", "CategoriaId", "dbo.Categorias", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Productos", "CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Productos", new[] { "CategoriaId" });
            DropColumn("dbo.Productos", "CategoriaId");
            DropTable("dbo.Categorias");
        }
    }
}
