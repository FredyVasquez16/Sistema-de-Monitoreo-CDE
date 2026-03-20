using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyFromClienteEmpresaToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "usuario_id",
                table: "asesores_clientes_empresas",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "ix_clientes_empresas_usuario_id",
                table: "clientes_empresas",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "fk_clientes_empresas_aspnetusers_usuario_id",
                table: "clientes_empresas",
                column: "usuario_id",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clientes_empresas_aspnetusers_usuario_id",
                table: "clientes_empresas");

            migrationBuilder.DropIndex(
                name: "ix_clientes_empresas_usuario_id",
                table: "clientes_empresas");

            migrationBuilder.AlterColumn<int>(
                name: "usuario_id",
                table: "asesores_clientes_empresas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
