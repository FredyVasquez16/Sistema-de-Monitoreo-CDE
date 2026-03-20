using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TipoComercioInternacionalGet
{
    public class ListaTipoCoomercioInternacional : IRequest<List<Dominio.TiposComerciosInternacional>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaTipoCoomercioInternacional, List<Dominio.TiposComerciosInternacional>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposComerciosInternacional>> Handle(ListaTipoCoomercioInternacional request, CancellationToken cancellationToken)
        {
            var tiposComercioInternacional = await _context.TiposComerciosInternacionales.ToListAsync();
            return tiposComercioInternacional;
        }
    }
}