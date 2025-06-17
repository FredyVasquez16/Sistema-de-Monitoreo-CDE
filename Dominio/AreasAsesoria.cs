using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("areas_asesorias")]
public class AreasAsesoria
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Asesorias> Asesorias { get; set; }
}