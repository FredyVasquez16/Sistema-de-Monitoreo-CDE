using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TiposEmpresaGet
{
    public class ListaTiposEmpresa : IRequest<List<Dominio.TiposEmpresa>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaTiposEmpresa, List<Dominio.TiposEmpresa>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposEmpresa>> Handle(ListaTiposEmpresa request, CancellationToken cancellationToken)
        {
            var tiposEmpresa = await _context.TiposEmpresas.ToListAsync();
            return tiposEmpresa;
        }
    }
}