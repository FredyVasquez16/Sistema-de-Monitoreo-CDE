namespace Aplicacion.Asesorias;

public class AsesoriaDto
{
    //Esto es lo que se va a mostrar en asesoria
    
    public int Id { get; set; }
    public string CodigoUnico { get; set; }
    public int ClienteId { get; set; }
    public DateTime FechaSesion { get; set; }
    public TimeOnly? TiempoContacto { get; set; }
    public int TipoContactoId { get; set; }
    public int AreaAsesoriaId { get; set; }
    public string? AyudaAdicional { get; set; }
    public string? Asunto { get; set; }
    public int FuenteFinanciamientoId { get; set; }
    public string? Centro { get; set; }
    public int? NumeroParticipantes { get; set; }
    public string? Notas { get; set; }
    public string? ReferidoA { get; set; }
    public string? DescripcionReferido { get; set; }
    public string? DescripcionDerivado { get; set; }
    public string? DescripcionAsesoriaEspecializada { get; set; }
    
    public ICollection<AsesorDto> Asesores { get; set; }
    public ICollection<AsesoriaContactoDto> Contactos { get; set; }
}