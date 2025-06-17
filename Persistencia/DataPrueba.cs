using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia;

public class DataPrueba
{
    public static async Task InsertarData(SistemaMonitoreaCdeContext context, UserManager<Usuario> usuarioManager)
    {
        if (!usuarioManager.Users.Any())
        {
            var usuario = new Usuario
            {
                NombreCompleto = "Fredy Vasquez",
                UserName = "fredy16v",
                Email = "fredy16v@gmail.com"
            };
            await usuarioManager.CreateAsync(usuario, "Test123@");
        }
    }
}