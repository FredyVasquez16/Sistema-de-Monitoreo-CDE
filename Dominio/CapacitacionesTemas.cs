using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("capacitaciones_temas")]
public class CapacitacionesTemas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("capacitacion_id")]
    public int CapacitacionId { get; set; }
    [Column("tema_id")]
    public int TemaId { get; set; }
    
    public virtual Capacitaciones Capacitacion { get; set; }
    public virtual Temas Tema { get; set; }
}