using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class TipoClienteNivelGet
{
    public class ListaTiposClienteNivel : IRequest<List<Dominio.TiposClienteNivel>>
    {
    }

    public class Manejador : IRequestHandler<ListaTiposClienteNivel, List<Dominio.TiposClienteNivel>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposClienteNivel>> Handle(ListaTiposClienteNivel request, CancellationToken cancellationToken)
        {
            var tiposClienteNivel = await _context.TiposClienteNiveles.ToListAsync();
            return tiposClienteNivel;
        }
    }
}