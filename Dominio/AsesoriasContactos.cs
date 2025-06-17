using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesorias_contactos")]
public class AsesoriasContactos
{
    [Column("id")]
    public int Id { get; set; }
    [Column("contacto_id")]
    public int ContactoId { get; set; }
    [Column("asesoria_id")]
    public int AsesoriaId { get; set; }
    [Column("cliente_empresa_id")]
    public int ClienteEmpresaId { get; set; }
    
    public virtual ClientesEmpresas ClienteEmpresa { get; set; }
    public virtual Asesorias Asesoria { get; set; }
}