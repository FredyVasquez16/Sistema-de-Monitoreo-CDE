using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesorias_asesores")]
public class AsesoriaAsesor
{
    [Column("id")]
    public int Id { get; set; }
    [Column("asesoria_id")]
    public int AsesoriaId { get; set; }
    [Column("asesor_id")]
    public string AsesorId { get; set; }
    
    public virtual Asesoria Asesoria { get; set; }
    public virtual Usuario Asesor { get; set; }
}