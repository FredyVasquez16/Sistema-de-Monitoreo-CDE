using System.Net;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            
            // Los contactos vinculados a esta empresa (contactos.empresa_cliente_id)
            // bloquean la eliminación (FK RESTRICT). Se desvinculan, sin borrar los contactos.
            var contactosVinculados = await _context.Contactos
                .Where(c => c.EmpresaClienteId == request.Id)
                .ToListAsync(cancellationToken);
            foreach (var contacto in contactosVinculados)
                contacto.EmpresaClienteId = null;
            
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