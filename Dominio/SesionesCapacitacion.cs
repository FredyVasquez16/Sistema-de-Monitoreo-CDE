using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("sesiones_capacitacion")]
public class SesionesCapacitacion
{
    [Column("id")]
    public int Id { get; set; }
    [Column("sesion_id")]
    public int SesionId { get; set; }
    [Column("capacitacion_id")]
    public int CapacitacionId { get; set; }
    
    public virtual Sesiones Sesion { get; set; }
    public virtual Capacitaciones Capacitacion { get; set; }
}