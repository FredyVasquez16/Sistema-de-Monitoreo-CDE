using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tipos_comercio_internacional")]
public class TiposComerciosInternacional
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
}