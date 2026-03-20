using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesores_clientes_empresas")]
public class AsesoresClientesEmpresas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("usuario_id")]
    public string UsuarioId { get; set; }
    [Column("cliente_empresa_id")]
    public int ClienteEmpresaId { get; set; }
}