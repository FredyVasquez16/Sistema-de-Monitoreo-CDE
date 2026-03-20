using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class CreacionColumnaCodigoUnicoTablaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_unico",
                table: "contactos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "codigo_unico",
                table: "clientes_empresas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "codigo_unico",
                table: "capacitaciones",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "codigounico",
                table: "aspnetusers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "codigo_unico",
                table: "asesorias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_unico",
                table: "contactos");

            migrationBuilder.DropColumn(
                name: "codigo_unico",
                table: "clientes_empresas");

            migrationBuilder.DropColumn(
                name: "codigo_unico",
                table: "capacitaciones");

            migrationBuilder.DropColumn(
                name: "codigounico",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "codigo_unico",
                table: "asesorias");
        }
    }
}
