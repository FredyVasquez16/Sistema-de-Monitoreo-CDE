using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tamano_empresas")]
public class TamanoEmpresas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
}