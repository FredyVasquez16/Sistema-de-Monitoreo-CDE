using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("temas")]
public class Temas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    //public virtual ICollection<Temas> TemaPrincipal { get; set; }
    public virtual ICollection<CapacitacionesTemas> CapacitacionesTemas { get; set; }
}