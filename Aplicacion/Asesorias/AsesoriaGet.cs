using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Asesorias;

public class AsesoriaGet
{
    public class ListaAsesorias : IRequest<List<Dominio.Asesorias>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaAsesorias, List<Dominio.Asesorias>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;

        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.Asesorias>> Handle(ListaAsesorias request, CancellationToken cancellationToken)
        {
            var asesorias = await _context.Asesorias.ToListAsync();
            return asesorias;
        }
    }
}