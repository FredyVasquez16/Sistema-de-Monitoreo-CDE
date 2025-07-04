using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tipos_contacto")]
public class TiposContacto
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Asesoria> Asesoria { get; set; }
}