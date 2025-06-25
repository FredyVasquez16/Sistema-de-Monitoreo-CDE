using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("clientes_empresas")]
public class ClientesEmpresas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("codigo_unico")]
    public string CodigoUnico { get; set; }
    [Column("nombre")]
    public string Nombre { get; set; }
    [Column("tipo_cliente_nivel_id")]
    public int TipoClienteNivelId { get; set; }
    [Column("contacto_primario_id")]
    public int ContactoPrimarioId { get; set; }
    [Column("tipo_cliente_estado_id")]
    public int TipoClienteEstadoId { get; set; }
    [Column("usuario_id")]
    public int UsuarioId { get; set; }
    [Column("servicio_solicitado_id")]
    public int ServicioSolicitadoId { get; set; }
    [Column("razon_social")]
    public string? RazonSocial { get; set; }
    [Column("telefono")]
    public int Telefono { get; set; }
    [Column("correo")]
    public string Correo { get; set; }
    [Column("pagina_web")]
    public string? PaginaWeb { get; set; }
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }
    [Column("direccion_fisica")]
    public string DireccionFisica { get; set; }
    [Column("ciudad")]
    public string Ciudad { get; set; }
    [Column("departamento")]
    public string Departamento { get; set; }
    [Column("tipo_organizacion_id")]
    public int TipoOrganizacionId { get; set; }
    [Column("tipo_empresa_id")]
    public int TipoEmpresaId { get; set; }
    [Column("tamano_empresa_id")]
    public int TamanoEmpresaId { get; set; }
    [Column("tipo_contabilidad_id")]
    public int TipoContabilidadId { get; set; }
    [Column("nivel_formalizacion_id")]
    public int NivelFormalizacionId { get; set; }
    [Column("participa_gremio")]
    public bool ParticipaGremio { get; set; }
    [Column("beneficiado_cde")]
    public bool BeneficiadoCde { get; set; }
    [Column("tipo_casos_en_proceso")]
    public string? TipoCasoEnProceso { get; set; }
    [Column("empleados_tiempo_completo")]
    public int EmpleadosTiempoCompleto { get; set; }
    [Column("empleados_medio_tiempo")]
    public int? EmpleadosMedioTiempo { get; set; }
    [Column("trabajadores_informales")]
    public int? TrabajadoresInformales { get; set; }
    [Column("negocio_en_linea")]
    public bool NegocioEnLinea { get; set; }
    [Column("negocio_en_casa")]
    public bool NegocioEnCasa { get; set; }
    [Column("comercio_internacional_id")]
    public int? ComercioInternacionalId { get; set; }
    [Column("paises_exporta")]
    public string? PaisExporta { get; set; }
    [Column("contrato_gobierno")]
    public bool ContratoGobierno { get; set; }
    [Column("zona_indigena")]
    public bool ZonaIndigena { get; set; }
    [Column("fuente_financiamiento_id")]
    public int FuenteFinanciamientoId { get; set; }
    [Column("subfuente_financiamiento_id")]
    public int? SubFuenteFinanciamientoId { get; set; }
    [Column("ingresos_brutos_anuales")]
    public double IngresosBrutosAnuales { get; set; }
    [Column("fecha_ingresos_brutos")]
    public DateTime FechaIngresosBrutos { get; set; }
    [Column("ingresos_exportaciones")]
    public double? IngresosExportaciones { get; set; }
    [Column("ganancias_perdidas_brutas")]
    public double? GananciasPerdidasBrutas { get; set; }
    [Column("fecha_ganancias_perdidas_brutas")]
    public DateTime FechaGananciasPerdidasBrutas { get; set; }
    [Column("descripcion_producto_servicio")]
    public string DescripcionProductoServicio { get; set; }
    [Column("areas_a_dominar")]
    public string? AreasADominar { get; set; }
    [Column("instrucciones")]
    public string? Instrucciones { get; set; }
    [Column("motivacion")]
    public string? Motivacion { get; set; }
    [Column("lugar_desarrollo_emprendimiento")]
    public string? LugarDesarrolloEmprendimiento { get; set; }
    [Column("obstaculos")]
    public string? Obstaculos { get; set; }
    [Column("fondo_concursable")]
    public string FondoConcursable { get; set; }
    [Column("estatus_inicial")]
    public string EstatusInicial { get; set; }
    [Column("estatus_actual")]
    public string EstatusActual { get; set; }
    [Column("fecha_establecimiento")]
    public DateTime FechaEstablecimiento { get; set; }
    [Column("nombre_propietario_id")]
    public int NombrePropietarioId { get; set; }
    [Column("genero_propietario")]
    public string GeneroPropietario { get; set; }
    [Column("ha_solicitado_credito")]
    public bool HaSolicitadoCredito { get; set; }
    [Column("como_solicito_credito")]
    public string? ComoSolicitoCredito { get; set; }
    [Column("porque_no_credito")]
    public string? PorQueNoCredito { get; set; }
    [Column("usa_pago_electronico")]
    public bool UsaPagoElectronico { get; set; }
    [Column("medios_pago")]
    public string? MediosPago { get; set; }
    [Column("notas")]
    public string? Notas { get; set; }
    
    [ForeignKey(nameof(FuenteFinanciamientoId))]
    public virtual FuenteFinanciamiento FuenteFinanciamiento { get; set; }

    [ForeignKey(nameof(SubFuenteFinanciamientoId))]
    public virtual FuenteFinanciamiento SubFuenteFinanciamiento { get; set; }
    [ForeignKey("NivelFormalizacionId")]
    public virtual NivelesFormalizacion NivelFormalizacion { get; set; }
    public virtual TiposContabilidad TipoContabilidad { get; set; }
    public virtual TamanoEmpresas TamanoEmpresa { get; set; }
    public virtual TiposEmpresa TipoEmpresa { get; set; }
    public virtual TiposOrganizacion TipoOrganizacion { get; set; }
    public virtual TiposClienteNivel TipoClienteNivel { get; set; }
    public virtual ServiciosSolicitados ServicioSolicitado { get; set; }
    public virtual TiposClientesEstado TipoClienteEstado { get; set; }
    public virtual TiposComerciosInternacional ComercioInternacional { get; set; }
    public virtual Contactos ContactoPrimario { get; set; }
    public virtual Contactos NombrePropietario { get; set; }
    
    public virtual ICollection<Asesorias> Asesorias { get; set; }
    public virtual ICollection<AsesoriasContactos> AsesoriasContactos { get; set; }
    public virtual ICollection<Contactos> Contactos { get; set; }
    public virtual ICollection<SesionesParticipantes> SesionesParticipantes { get; set; }
}