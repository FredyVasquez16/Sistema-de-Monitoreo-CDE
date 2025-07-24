using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class UsuarioRolDelete
{
    public class EjecutaUsuarioRolDelete : IRequest
    {
        public string Username { get; set; }
        public string RolNombre { get; set; }
    }
    
    public class ValidaEjecutaUsuarioRolDelete : AbstractValidator<EjecutaUsuarioRolDelete>
    {
        public ValidaEjecutaUsuarioRolDelete()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.RolNombre).NotEmpty().WithMessage("El nombre del rol es obligatorio");
        }
    }
    
    public class Manejador : IRequestHandler<EjecutaUsuarioRolDelete>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(EjecutaUsuarioRolDelete request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                throw new Exception($"El usuario {request.Username} no existe");
            }

            var roleExists = await _roleManager.FindByNameAsync(request.RolNombre);
            if (roleExists == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,
                    $"El rol {request.RolNombre} no existe");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RolNombre);
            if (result.Succeeded)
            {
                return Unit.Value;
            }
            
            throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError,
                "No se pudo eliminar el rol del usuario");
        }
    }
}