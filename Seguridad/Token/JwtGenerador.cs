using System.Security.Claims;
using Aplicacion.Contratos;
using Dominio;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad.Token;

public class JwtGenerador : IJwtGenerador
{
    public string CrearToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("T7QdU8+wCBgbefv6iY5QRUo5FbRnbatA7NO5IK56zjDp5jWDZb9F36JqCWAL/IzD1XDhV+q3kDUKkZo59IdT4Q==\n"));
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var tokenDescripcion = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credenciales
        };
        
        var tokenManejador = new JwtSecurityTokenHandler();
        var token = tokenManejador.CreateToken(tokenDescripcion);
        
        return tokenManejador.WriteToken(token);
    }
}