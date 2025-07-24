using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad;

public class RolLista
{
    public class EjecutaLista : IRequest<List<IdentityRole>>
    {
    }
    
    public class Manejador : IRequestHandler<EjecutaLista, List<IdentityRole>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityRole>> Handle(EjecutaLista request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }
    }
}