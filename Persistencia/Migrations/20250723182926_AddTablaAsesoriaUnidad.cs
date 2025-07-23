using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddTablaAsesoriaUnidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unidad",
                table: "asesorias");

            migrationBuilder.CreateTable(
                name: "asesorias_unidades",
                columns: table => new
                {
                    asesoria_id = table.Column<int>(type: "integer", nullable: false),
                    unidad_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asesorias_unidades", x => new { x.asesoria_id, x.unidad_id });
                    table.ForeignKey(
                        name: "fk_asesorias_unidades_asesorias_asesoria_id",
                        column: x => x.asesoria_id,
                        principalTable: "asesorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asesorias_unidades_unidades_unidad_id",
                        column: x => x.unidad_id,
                        principalTable: "unidades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asesorias_unidades_unidad_id",
                table: "asesorias_unidades",
                column: "unidad_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asesorias_unidades");

            migrationBuilder.AddColumn<string>(
                name: "unidad",
                table: "asesorias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
