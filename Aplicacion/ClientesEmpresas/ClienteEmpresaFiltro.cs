using Aplicacion.ClientesEmpresas.DTOs;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaFiltro
{
    public class ClienteEmpresaFiltroEjecuta : IRequest<List<ClienteEmpresaFiltroDto>>
    {
        public string TerminoBusqueda { get; set; }
    }

    public class Manejador : IRequestHandler<ClienteEmpresaFiltroEjecuta, List<ClienteEmpresaFiltroDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteEmpresaFiltroDto>> Handle(ClienteEmpresaFiltroEjecuta request, CancellationToken cancellationToken)
        {
            var termino = request.TerminoBusqueda?.Trim().ToLower() ?? string.Empty;

            var clientes = await _context.ClientesEmpresas
                .Where(c => string.IsNullOrEmpty(termino) ||
                            (c.RazonSocial ?? c.Nombre).ToLower().Contains(termino))
                .Take(10) // Limitamos los resultados
                .Select(c => new ClienteEmpresaFiltroDto
                {
                    Id = c.Id,
                    RazonSocial = c.RazonSocial,
                    Nombre = c.Nombre
                })
                .ToListAsync(cancellationToken);

            return clientes;
        }
    }
}
