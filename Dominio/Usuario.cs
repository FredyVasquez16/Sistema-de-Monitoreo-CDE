using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dominio;

public class Usuario : IdentityUser
{
    public string NombreCompleto { get; set; }
}