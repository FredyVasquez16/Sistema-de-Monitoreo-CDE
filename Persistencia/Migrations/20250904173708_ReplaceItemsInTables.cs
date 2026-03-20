using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceItemsInTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ciudad",
                table: "contactos",
                newName: "municipio");

            migrationBuilder.RenameColumn(
                name: "ciudad",
                table: "clientes_empresas",
                newName: "municipio");

            migrationBuilder.RenameColumn(
                name: "ciudad",
                table: "capacitaciones",
                newName: "municipio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "municipio",
                table: "contactos",
                newName: "ciudad");

            migrationBuilder.RenameColumn(
                name: "municipio",
                table: "clientes_empresas",
                newName: "ciudad");

            migrationBuilder.RenameColumn(
                name: "municipio",
                table: "capacitaciones",
                newName: "ciudad");
        }
    }
}
