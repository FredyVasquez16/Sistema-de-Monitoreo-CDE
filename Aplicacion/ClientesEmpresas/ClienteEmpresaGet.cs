using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaGet
{
    public class ListaClientesEmpresas : IRequest<List<Dominio.ClientesEmpresas>>
    {
    }

    public class Manejador : IRequestHandler<ListaClientesEmpresas, List<Dominio.ClientesEmpresas>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.ClientesEmpresas>> Handle(ListaClientesEmpresas request, CancellationToken cancellationToken)
        {
            var clientesEmpresas = await _context.ClientesEmpresas.ToListAsync();
            return clientesEmpresas;
        }
    }
}    