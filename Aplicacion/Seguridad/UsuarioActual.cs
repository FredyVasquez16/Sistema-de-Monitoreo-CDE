using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class UsuarioActual
{
    public class UsuarioActualEjecuta : IRequest<UsuarioData>{}
    
    public class Manejador : IRequestHandler<UsuarioActualEjecuta, UsuarioData>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IUsuarioSesion _usuarioSesion;
        
        public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
        {
            _userManager = userManager;
            _jwtGenerador = jwtGenerador;
            _usuarioSesion = usuarioSesion;
        }
        
        public async Task<UsuarioData> Handle(UsuarioActualEjecuta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
            if (usuario == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "Usuario no encontrado" });
            }
            //var roles = await _userManager.GetRolesAsync(usuario);
            return new UsuarioData
            {
                NombreCompleto = usuario.NombreCompleto,
                UserName = usuario.UserName,
                Email = usuario.Email,
                Token = _jwtGenerador.CrearToken(usuario)
                //Roles = roles.ToList()
            };
        }
    }
}