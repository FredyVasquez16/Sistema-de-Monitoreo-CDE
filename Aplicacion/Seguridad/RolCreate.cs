using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad;

public class RolCreate
{
    public class EjecutaCreate : IRequest
    {
        public string Nombre { get; set; }
    }
    
    public class ValidaEjecuta : AbstractValidator<EjecutaCreate>
    {
        public ValidaEjecuta()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre del rol es obligatorio");
        }
    }
    
    public class Manejador : IRequestHandler<EjecutaCreate>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public Manejador(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public async Task<Unit> Handle(EjecutaCreate request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Nombre);
            if (role != null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest,
                    $"El rol {request.Nombre} ya existe");
            }
            
            var resultado = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));
            if (resultado.Succeeded)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                "No se pudo crear el rol");
        }
    }
}