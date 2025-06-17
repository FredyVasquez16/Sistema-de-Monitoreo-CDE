using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionGet
{
    public class ListaCapacitaciones : IRequest<List<Dominio.Capacitaciones>>
    {
    }

    public class Manejador : IRequestHandler<ListaCapacitaciones, List<Dominio.Capacitaciones>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<List<Dominio.Capacitaciones>> Handle(ListaCapacitaciones request, CancellationToken cancellationToken)
        {
            var capacitaciones = await _context.Capacitaciones.ToListAsync();
            return capacitaciones;
        }
    }
}