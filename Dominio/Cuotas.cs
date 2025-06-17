using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("cuotas")]
public class Cuotas
{
    [Column("id")]
    public int Id { get; set; }
    [Column("tipo")]
    public string Tipo { get; set; }
    [Column("nombre")]
    public string Nombre { get; set; }
    [Column("precio")]
    public decimal Precio { get; set; }
    [Column("publicado")]
    public bool Publicado { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Sesiones> Sesiones { get; set; }
}