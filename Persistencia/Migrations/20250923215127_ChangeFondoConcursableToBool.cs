// Persistencia/Migrations/..._ChangeFondoConcursableToBool.cs

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class ChangeFondoConcursableToBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Usamos SQL crudo para darle a PostgreSQL instrucciones explícitas de conversión
            // con una sentencia CASE.
            migrationBuilder.Sql(@"
                ALTER TABLE clientes_empresas 
                ALTER COLUMN fondo_concursable TYPE BOOLEAN 
                USING CASE 
                    WHEN lower(fondo_concursable) = 'sí' THEN TRUE 
                    WHEN lower(fondo_concursable) = 'si' THEN TRUE 
                    WHEN lower(fondo_concursable) = 'true' THEN TRUE 
                    ELSE FALSE 
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Para revertir, hacemos la conversión inversa de boolean a text.
            migrationBuilder.Sql(@"
                ALTER TABLE clientes_empresas 
                ALTER COLUMN fondo_concursable TYPE TEXT 
                USING CASE 
                    WHEN fondo_concursable = TRUE THEN 'Sí' 
                    ELSE 'No' 
                END;
            ");
        }
    }
}