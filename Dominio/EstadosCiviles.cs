using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("estados_civiles")]
public class EstadosCiviles
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Contactos> Contactos { get; set; }
}