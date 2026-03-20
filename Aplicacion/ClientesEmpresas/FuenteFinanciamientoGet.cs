using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class FuenteFinanciamientoGet
{
    public class ListaFuenteFinanciamiento : IRequest<List<Dominio.FuenteFinanciamiento>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaFuenteFinanciamiento, List<Dominio.FuenteFinanciamiento>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.FuenteFinanciamiento>> Handle(ListaFuenteFinanciamiento request, CancellationToken cancellationToken)
        {
            var fuenteFinanciamiento = await _context.FuenteFinanciamientos.ToListAsync();
            return fuenteFinanciamiento;
        }
    }
}