using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class UsuarioRolAdd
{
    public class EjecutaAdd : IRequest
    {
        public string username { get; set; }
        public string RolNombre { get; set; }
    }
    
    public class ValidaEjecuta : AbstractValidator<EjecutaAdd>
    {
        public ValidaEjecuta()
        {
            RuleFor(x => x.username).NotEmpty().WithMessage("El nombre de usuario es obligatorio");
            RuleFor(x => x.RolNombre).NotEmpty().WithMessage("El nombre del rol es obligatorio");
        }
    }
    
    public class Manejador : IRequestHandler<EjecutaAdd>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(EjecutaAdd request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.username);
            if (user == null)
            {
                throw new Exception($"El usuario {request.username} no existe");
            }

            var roleExists = await _roleManager.FindByNameAsync(request.RolNombre);
            if (roleExists == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,
                    $"El rol {request.RolNombre} no existe");
            }

            var result = await _userManager.AddToRoleAsync(user, request.RolNombre);
            if (result.Succeeded)
            {
                return Unit.Value;
            }
            
            throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError,
                "No se pudo asignar el rol al usuario");
        }
    }
}