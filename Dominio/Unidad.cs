using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("unidades")]
public class Unidad
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    
    public ICollection<UsuarioUnidad> UsuariosUnidades { get; set; }
    public virtual ICollection<AsesoriaUnidad> AsesoriasUnidades { get; set; }
}