using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TamanoEmpresaGet
{
    public class ListaTamanoEmpresa : IRequest<List<Dominio.TamanoEmpresas>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaTamanoEmpresa, List<Dominio.TamanoEmpresas>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TamanoEmpresas>> Handle(ListaTamanoEmpresa request, CancellationToken cancellationToken)
        {
            var tamanoEmpresa = await _context.TamanoEmpresas.ToListAsync();
            return tamanoEmpresa;
        }
    }
}