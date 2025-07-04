using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("categorias_laborales")]
public class CategoriasLaborales
{
    [Column("id")]
    public int Id { get; set; }
    [Column("descripcion")]
    public string Descripcion { get; set; }
    
    public virtual ICollection<Contacto> Contactos { get; set; }
}