using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("niveles_estudio")]
public class NivelesEstudio
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Contactos> Contactos { get; set; }
}