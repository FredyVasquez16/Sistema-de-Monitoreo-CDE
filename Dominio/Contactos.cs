using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("contactos")]
public class Contactos
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }

    [Column("apellido")]
    public string Apellido { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Column("dni")]
    public string Dni { get; set; }

    [Column("nacionalidad")]
    public string Nacionalidad { get; set; }

    [Column("genero")]
    public string Genero { get; set; }

    [Column("telefono")]
    public int Telefono { get; set; }

    [Column("correo")]
    public string Correo { get; set; }

    [Column("rtn")]
    public string Rtn { get; set; }

    [Column("direccion")]
    public string Direccion { get; set; }

    [Column("ciudad")]
    public string Ciudad { get; set; }

    [Column("departamento")]
    public string Departamento { get; set; }

    [Column("cargo")]
    public string Cargo { get; set; }

    [Column("estado_civil_id")]
    public int? EstadoCivilId { get; set; }

    [Column("nivel_estudio_id")]
    public int NivelEstudioId { get; set; }

    [Column("categoria_laboral_id")]
    public int? CategoriaLaboralId { get; set; }

    [Column("posee_negocio")]
    public bool PoseeNegocio { get; set; }

    [Column("nombre_etnia")]
    public string? NombreEtnia { get; set; }

    [Column("localidad_etnica")]
    public string? LocalidadEtnica { get; set; }

    [Column("contacto_discapacidad")]
    public int? ContactoDiscapacidad { get; set; }

    [Column("integrantes_totales_familia")]
    public int? IntegrantesTotalesFamilia { get; set; }

    [Column("numero_hijos")]
    public int? NumeroHijos { get; set; }

    [Column("numero_hijas")]
    public int? NumeroHijas { get; set; }

    [Column("rol_contacto_familiar")]
    public string? RolContactoFamiliar { get; set; }

    [Column("centro")]
    public string? Centro { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("empresa_cliente_id")]
    public int? EmpresaClienteId { get; set; }

    // Propiedades de navegación (sin [Column], ya que no se almacenan como campos)
    public virtual EstadosCiviles EstadoCivil { get; set; }
    public virtual NivelesEstudio NivelEstudio { get; set; }
    public virtual CategoriasLaborales CategoriaLaboral { get; set; }
    [ForeignKey("EmpresaClienteId")]
    public virtual ClientesEmpresas ClienteEmpresa { get; set; }

    //public virtual ICollection<ClientesEmpresas> ClientesEmpresas { get; set; }
    public virtual ICollection<SesionesParticipantes> SesionesParticipantes { get; set; }
}