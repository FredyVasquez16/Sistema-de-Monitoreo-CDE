using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TiposOrganizacionGet
{
    public class ListaTiposOrganizacion : IRequest<List<Dominio.TiposOrganizacion>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaTiposOrganizacion, List<Dominio.TiposOrganizacion>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposOrganizacion>> Handle(ListaTiposOrganizacion request, CancellationToken cancellationToken)
        {
            var tiposOrganizacion = await _context.TiposOrganizaciones.ToListAsync();
            return tiposOrganizacion;
        }
    }
}