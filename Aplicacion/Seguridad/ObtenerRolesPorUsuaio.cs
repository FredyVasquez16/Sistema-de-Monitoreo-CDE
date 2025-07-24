using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class ObtenerRolesPorUsuaio
{
    public class EjecutaObtenerRolesPorUsuario : IRequest<List<string>>
    {
        public string Username { get; set; }
    }

    public class Manejador : IRequestHandler<EjecutaObtenerRolesPorUsuario, List<string>>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<string>> Handle(EjecutaObtenerRolesPorUsuario request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(request.Username);
            if (usuario == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,
                    $"El usuario {request.Username} no existe");
            }

            var roles = await _userManager.GetRolesAsync(usuario);
            //return roles.ToList();
            return new List<string>(roles); // Alternativa si se quiere una lista nueva
        }
    }
}