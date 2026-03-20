using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TipoContabilidadGet
{
    public class ListaTiposContabilidad : IRequest<List<Dominio.TiposContabilidad>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaTiposContabilidad, List<Dominio.TiposContabilidad>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposContabilidad>> Handle(ListaTiposContabilidad request, CancellationToken cancellationToken)
        {
            var tiposContabilidad = await _context.TiposContabilidades.ToListAsync();
            return tiposContabilidad;
        }
    }
}