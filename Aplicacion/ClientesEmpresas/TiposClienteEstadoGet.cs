using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class TiposClienteEstadoGet
{
    public class ListaTiposClienteEstado : IRequest<List<Dominio.TiposClientesEstado>>
    {
    }

    public class Manejador : IRequestHandler<ListaTiposClienteEstado, List<Dominio.TiposClientesEstado>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.TiposClientesEstado>> Handle(ListaTiposClienteEstado request, CancellationToken cancellationToken)
        {
            var tiposClienteEstado = await _context.TiposClientesEstados.ToListAsync();
            return tiposClienteEstado;
        }
    }
}