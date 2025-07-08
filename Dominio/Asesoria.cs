using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("asesorias")]
public class Asesoria
{
    [Column("id")]
    public int Id { get; set; }
    [Column("codigo_unico")]
    public string CodigoUnico { get; set; }
    [Column("cliente_id")]
    public int ClienteId { get; set; }
    [Column("fecha_sesion")]
    public DateTime FechaSesion { get; set; }
    [Column("tiempo_contacto")]
    public TimeOnly? TiempoContacto { get; set; }
    [Column("tipo_contacto_id")]
    public int TipoContactoId { get; set; }
    [Column("area_asesoria_id")]
    public int AreaAsesoriaId { get; set; }
    [Column("ayuda_adicional")]
    public string? AyudaAdicional { get; set; }
    [Column("asunto")]
    public string? Asunto { get; set; }
    [Column("fuente_financiamiento_id")]
    public  int FuenteFinanciamientoId { get; set; }
    [Column("centro")]
    public string? Centro { get; set; }
    [Column("numero_participantes")]
    public int ?NumeroParticipantes { get; set; }
    [Column("notas")]
    public string? Notas { get; set; }
    [Column("referido_a")]
    public string? ReferidoA { get; set; }
    [Column("descripcion_referido")]
    public string? DescripcionReferido { get; set; }
    [Column("descripcion_derivado")]
    public string? DescripcionDerivado { get; set; }
    [Column("descripcion_asesoria_especializada")]
    public string? DescripcionAsesoriaEspecializada { get; set; }
    [Column("unidad")]
    public string Unidad { get; set; }
    
    //public byte[]? ArchivosAdjuntos { get; set; }
    
    public virtual TiposContacto TiposContacto { get; set; }
    public virtual AreasAsesoria AreaAsesoria { get; set; }
    public virtual FuenteFinanciamiento FuenteFinanciamiento { get; set; }
    public virtual ClientesEmpresas Cliente { get; set; }
    
    public virtual ICollection<AsesoriaContacto> AsesoriasContactos { get; set; }
    public virtual ICollection<AsesoriasArchivos> Archivos { get; set; }
    public virtual ICollection<AsesoriaAsesor> Asesores { get; set; }
}