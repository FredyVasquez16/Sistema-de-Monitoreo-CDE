using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddPropiedadAsesoriaContactos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_asesorias_contactos_contacto_id",
                table: "asesorias_contactos",
                column: "contacto_id");

            migrationBuilder.AddForeignKey(
                name: "fk_asesorias_contactos_contactos_contacto_id",
                table: "asesorias_contactos",
                column: "contacto_id",
                principalTable: "contactos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asesorias_contactos_contactos_contacto_id",
                table: "asesorias_contactos");

            migrationBuilder.DropIndex(
                name: "ix_asesorias_contactos_contacto_id",
                table: "asesorias_contactos");
        }
    }
}
