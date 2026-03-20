using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaGetById
{
    public class ClienteEmpresaUnico : IRequest<Dominio.ClientesEmpresas>
    {
        public int Id { get; set; }
    }

    public class Manejador : IRequestHandler<ClienteEmpresaUnico, Dominio.ClientesEmpresas>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Dominio.ClientesEmpresas> Handle(ClienteEmpresaUnico request, CancellationToken cancellationToken)
        {
            var clienteEmpresa = await _context.ClientesEmpresas.FindAsync(request.Id);
            
            var cliente = await _context.ClientesEmpresas
                .Include(c => c.ContactoPrimario)
                .Include(c => c.Usuario)
                .Include(c => c.NombrePropietario)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (clienteEmpresa == null)
            {
                throw new ManejadorError.ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, 
                    new { mensaje = "El cliente o empresa no existe o no se ha encontrado." });
            }
            return clienteEmpresa;
        }
    }
}