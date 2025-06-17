using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tipos_empresa")]
public class TiposEmpresa
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
}