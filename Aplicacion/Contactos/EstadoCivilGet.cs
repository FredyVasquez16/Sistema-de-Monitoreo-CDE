using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class EstadoCivilGet
{
    public class EstadoCivilGetEjecuta : IRequest<List<EstadosCiviles>>
    {
        
    }

    public class Manejador : IRequestHandler<EstadoCivilGetEjecuta, List<EstadosCiviles>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<EstadosCiviles>> Handle(EstadoCivilGetEjecuta request, CancellationToken cancellationToken)
        {
            var estadosCiviles = await _context.EstadosCiviles.ToListAsync();
            return estadosCiviles;
        }
    }
}