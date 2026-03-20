namespace Aplicacion.ClientesEmpresas.DTOs;

public class ClienteEmpresaDto
{
    public int Id { get; set; }
    public string CodigoUnico { get; set; }
    public string Nombre { get; set; }
        
    // --- CAMPOS TRANSFORMADOS ---
    public int contactoPrimarioId { get; set; } // Mantener el ID original para referencias internas
    public string usuarioId { get; set; } // Mantener el ID original para referencias
    public string ContactoPrimarioNombre { get; set; } // En lugar de contactoPrimarioId
    public string AsesorPrincipalNombre { get; set; } // En lugar de usuarioId

    // --- Mantenemos algunos otros campos que la lista podría necesitar ---
    public int Telefono { get; set; }
    public string Correo { get; set; }
    public string EstatusActual { get; set; }
}