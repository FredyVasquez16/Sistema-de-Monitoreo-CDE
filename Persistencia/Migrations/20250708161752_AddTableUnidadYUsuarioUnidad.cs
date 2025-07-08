using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddTableUnidadYUsuarioUnidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "unidad",
                table: "capacitaciones",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "unidad",
                table: "asesorias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "unidades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unidades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_unidades",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario_id = table.Column<string>(type: "text", nullable: false),
                    unidad_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios_unidades", x => x.id);
                    table.ForeignKey(
                        name: "fk_usuarios_unidades_aspnetusers_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_usuarios_unidades_unidades_unidad_id",
                        column: x => x.unidad_id,
                        principalTable: "unidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_unidades_unidad_id",
                table: "usuarios_unidades",
                column: "unidad_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuarios_unidades_usuario_id",
                table: "usuarios_unidades",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuarios_unidades");

            migrationBuilder.DropTable(
                name: "unidades");

            migrationBuilder.DropColumn(
                name: "unidad",
                table: "capacitaciones");

            migrationBuilder.DropColumn(
                name: "unidad",
                table: "asesorias");
        }
    }
}
