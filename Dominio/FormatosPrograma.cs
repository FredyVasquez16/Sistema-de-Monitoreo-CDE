using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("formatos_programa")]
public class FormatosPrograma
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Capacitaciones> Capacitaciones { get; set; }
}