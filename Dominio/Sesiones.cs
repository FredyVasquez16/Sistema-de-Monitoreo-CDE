using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("sesiones")]
public class Sesiones
{
    [Column("id")]
    public int Id { get; set; }
    [Column("titulo")]
    public string Titulo { get; set; }
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("fecha_final")]
    public DateTime FechaFinal { get; set; }
    [Column("cuota_id")]
    public int CuotaId { get; set; }
    
    public virtual ICollection<Cuotas> Cuotas { get; set; }
    public virtual ICollection<SesionesCapacitacion> SesionesCapacitaciones { get; set; }
    public virtual ICollection<SesionesParticipantes> SesionesParticipantes { get; set; }
}