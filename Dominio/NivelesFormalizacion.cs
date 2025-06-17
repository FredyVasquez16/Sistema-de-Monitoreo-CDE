using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("niveles_formalizacion")]
public class NivelesFormalizacion
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
}