using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Dominio;

public class Usuario : IdentityUser
{
    [Column("codigounico")]
    public string CodigoUnico { get; set; }
    public string NombreCompleto { get; set; }
    
    public ICollection<UsuarioUnidad> UsuariosUnidades { get; set; } 
}