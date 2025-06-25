using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("capacitaciones")]
public class Capacitaciones
{
    [Column("id")]
    public int Id { get; set; }
    [Column("codigo_unico")]
    public string CodigoUnico { get; set; }
    [Column("tipo_id")]
    public int TipoId { get; set; }
    [Column("titulo")]
    public string Titulo { get; set; }
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("fecha_cierre")]
    public DateTime FechaCierre { get; set; }
    [Column("fecha_informes")]
    public DateTime FechaInformes { get; set; }
    [Column("hora_programada")]
    public TimeOnly HoraProgramada { get; set; }
    [Column("total_horas")]
    public int? TotalHoras { get; set; }
    [Column("descripcion")]
    public string? Descripcion { get; set; }
    [Column("tema_principal_id")]
    public int TemaPrincipalId { get; set; }
    [Column("formato_programa_id")]
    public int FormatoProgramaId { get; set; }
    [Column("estado")]
    public string? Estado { get; set; }
    [Column("numero_max_participantes")]
    public int? NumeroMaxParticipantes { get; set; }
    [Column("numero_sesiones")]
    public int NumeroSesiones { get; set; }
    [Column("direccion")]
    public string Direccion { get; set; }
    [Column("ciudad")]
    public string Ciudad { get; set; }
    [Column("departamento")]
    public string Departamento { get; set; }
    [Column("lugar_desarrollo")]
    public string LugarDesarrollo { get; set; }
    [Column("centro")]
    public string? Centro { get; set; }
    [Column("patrocinio_centro")]
    public bool PatrociniosCentro { get; set; }
    [Column("co_patrocinios")]
    public string? CoPatrocinios { get; set; }
    [Column("recursos")]
    public string? Recursos { get; set; }
    [Column("contacto")]
    public string? Contacto { get; set; }   
    [Column("correo_contacto")]
    public string? CorreoContacto { get; set; }
    [Column("telefono_contacto")]
    public int? TelefonoContacto { get; set; }
    [Column("idioma")]
    public string? Idioma { get; set; }
    [Column("unidad_historica")]
    public string? UnidadHistorica { get; set; }
    [Column("fuente_financiamiento_id")]
    public int? FuenteFinanciamientoId { get; set; }
    [Column("instrucciones_asistente")]
    public string? IntruccionesAsistente { get; set; }
    [Column("notas")]
    public string? Notas { get; set; }
    
    public virtual FuenteFinanciamiento FuenteFinanciamiento { get; set; }
    public virtual Tipos Tipo { get; set; }
    public virtual Temas TemaPrincipal { get; set; }
    public virtual FormatosPrograma FormatoPrograma { get; set; }
    
    public virtual ICollection<CapacitacionesTemas> CapacitacionesTemas { get; set; }
    public virtual ICollection<SesionesCapacitacion> SesionesCapacitacion { get; set; }
    public virtual ICollection<CapacitacionesArchivos> Archivos { get; set; }
}