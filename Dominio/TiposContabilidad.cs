using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tipos_contabilidad")]
public class TiposContabilidad
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
}