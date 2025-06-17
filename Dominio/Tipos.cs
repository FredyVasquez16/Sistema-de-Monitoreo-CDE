using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("tipos")]
public class Tipos
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Capacitaciones> Capacitaciones { get; set; }
}