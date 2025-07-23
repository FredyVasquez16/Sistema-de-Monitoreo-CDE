using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesorias_unidades")]
public class AsesoriaUnidad
{
    [Column("asesoria_id")]
    public int AsesoriaId { get; set; }

    [Column("unidad_id")]
    public int UnidadId { get; set; }

    public virtual Asesoria Asesoria { get; set; }
    public virtual Unidad Unidad { get; set; }
}