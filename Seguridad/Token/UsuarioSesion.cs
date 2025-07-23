using System.Security.Claims;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;

namespace Seguridad.Token;

public class UsuarioSesion : IUsuarioSesion
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string ObtenerUsuarioSesion()
    {
        var userName = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return userName;
    }
    
    public string ObtenerUsuarioId()
    {
        // Aquí estamos extrayendo el ID del usuario desde el Claims del token JWT
        var userId = _httpContextAccessor.HttpContext.User?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        return userId;
    }
    
    /*public string ObtenerUsuarioId()
    {
        var claims = _httpContextAccessor.HttpContext?.User?.Claims;
        var userId = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
                      ?? claims?.FirstOrDefault(x => x.Type == "nameid")?.Value;
        return userId;
    }*/

    /*public string ObtenerNameId()
    {
        var claims = _httpContextAccessor.HttpContext?.User?.Claims;
        var userName = claims?.FirstOrDefault(x => x.Type == "nameid")?.Value
                       ?? claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return userName;
    }*/
}