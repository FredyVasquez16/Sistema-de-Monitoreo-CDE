using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesorias_archivos")]
public class AsesoriasArchivos
{
    [Column("id")]
    public int Id { get; set; }

    [Column("asesoria_id")]
    public int AsesoriaId { get; set; }

    [Column("nombre_original")]
    public string NombreOriginal { get; set; }

    [Column("contenido")]
    public byte[] Contenido { get; set; }

    [Column("tipo_mime")]
    public string TipoMime { get; set; }

    [Column("fecha_subida")]
    public DateTime FechaSubida { get; set; }

    public virtual Asesorias Asesoria { get; set; }
}