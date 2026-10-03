namespace Aplicacion.Asesorias;

public class AsesoriaContactoDto
{
    public int Id { get; set; }
    public int ContactoId { get; set; }
    public int AsesoriaId { get; set; }
    public int ClienteEmpresaId { get; set; }
    public string? ContactoNombre { get; set; }
    public string? ClienteEmpresaNombre { get; set; }
}