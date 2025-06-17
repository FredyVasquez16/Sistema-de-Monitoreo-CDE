using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaDelete
{
    public class ClienteEmpresaDeleteEjecuta : IRequest
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<ClienteEmpresaDeleteEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(ClienteEmpresaDeleteEjecuta request, CancellationToken cancellationToken)
        {
            var clienteEmpresa = await _context.ClientesEmpresas.FindAsync(request.Id);
            if (clienteEmpresa == null)
            {
                //throw new Exception("Cliente o empresa no encontrado");
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El cliente o empresa no existe o no se ha encontrado." });
            }
            
            _context.Remove(clienteEmpresa);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar el cliente o empresa." } );
        }
    }
}