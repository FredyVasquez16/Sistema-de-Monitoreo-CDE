using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio;

[Table("usuarios_unidades")]
public class UsuarioUnidad
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("usuario_id")]
    public string  UsuarioId { get; set; }
    
    [Column("unidad_id")]
    public int UnidadId { get; set; }
    
    public virtual Unidad Unidad { get; set; }
    public virtual Usuario Usuario { get; set; }
}