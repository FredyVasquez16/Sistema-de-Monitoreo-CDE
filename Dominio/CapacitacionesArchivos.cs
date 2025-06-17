using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("capacitaciones_archivos")]
public class CapacitacionesArchivos
{
    [Column("id")]
    public int Id { get; set; }

    [Column("capacitacion_id")]
    public int CapacitacionId { get; set; }

    [Column("nombre_original")]
    public string NombreOriginal { get; set; }

    [Column("contenido")]
    public byte[] Contenido { get; set; }

    [Column("tipo_mime")]
    public string TipoMime { get; set; }

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; }

    public virtual Capacitaciones Capacitacion { get; set; }
}