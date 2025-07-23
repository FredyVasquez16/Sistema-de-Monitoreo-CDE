using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class RolDelete
{
    public class EjecutaDelete : IRequest
    {
        public string Nombre { get; set; }
    }

    public class EjecutaValida : AbstractValidator<EjecutaDelete>
    {
        public EjecutaValida()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio");
        }
    }
    
    public class Manejador : IRequestHandler<EjecutaDelete>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public Manejador(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(EjecutaDelete request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Nombre);
            if (role == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,
                    $"El rol {request.Nombre} no existe");
            }

            var resultado = await _roleManager.DeleteAsync(role);
            if (resultado.Succeeded)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(System.Net.HttpStatusCode.InternalServerError,
                "No se pudo eliminar el rol");
        }
    }
}