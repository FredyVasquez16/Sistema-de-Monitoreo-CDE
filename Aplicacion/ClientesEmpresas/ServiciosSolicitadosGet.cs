using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class ServiciosSolicitadosGet
{
    public class ListaServiciosSolicitados : IRequest<List<Dominio.ServiciosSolicitados>>
    {
    }

    public class Manejador : IRequestHandler<ListaServiciosSolicitados, List<Dominio.ServiciosSolicitados>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.ServiciosSolicitados>> Handle(ListaServiciosSolicitados request, CancellationToken cancellationToken)
        {
            var serviciosSolicitados = await _context.ServiciosSolicitados.ToListAsync();
            return serviciosSolicitados;
        }
    }
}