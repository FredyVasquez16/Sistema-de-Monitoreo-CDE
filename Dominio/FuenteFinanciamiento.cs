using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("fuentes_financiamiento")]
public class FuenteFinanciamiento
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<ClientesEmpresas> ClientesEmpresasFuente { get; set; }
    public virtual ICollection<ClientesEmpresas> ClientesEmpresasSubfuente { get; set; }
    public virtual ICollection<Asesorias> Asesorias { get; set; }
    public virtual ICollection<Capacitaciones> Capacitaciones { get; set; }
}