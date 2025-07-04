using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("sesiones_participantes")]
public class SesionesParticipantes
{
    [Column("id")]
    public int Id { get; set; }
    [Column("sesion_id")]
    public int SesionId { get; set; }
    [Column("contacto_id")]
    public int ContactoId { get; set; }
    [Column("cliente_empresa_id")]
    public int ClienteEmpresaId { get; set; }
    
    public virtual Sesiones Sesion { get; set; }
    public virtual Contacto Contacto { get; set; }
    public virtual ClientesEmpresas ClienteEmpresa { get; set; }
}