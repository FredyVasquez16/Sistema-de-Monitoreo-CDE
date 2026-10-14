using System.Net;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class ContactoDelete
{
    public class ContactoDeleteEjecuta : IRequest
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<ContactoDeleteEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ContactoDeleteEjecuta request, CancellationToken cancellationToken)
        {
            var contacto = await _context.Contactos.FindAsync(request.Id);
            if (contacto == null)
            {
                //throw new Exception("Contacto no encontrado");
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El contacto no existe o no se ha encontrado."} );
            }
            // Si el contacto es contacto primario de una empresa, la FK RESTRICT lo bloquearía:
            // se impide con un mensaje claro en lugar de una excepción de base de datos.
            var esPrimarioDeUnaEmpresa = await _context.ClientesEmpresas
                .AnyAsync(c => c.ContactoPrimarioId == request.Id, cancellationToken);
            if (esPrimarioDeUnaEmpresa)
            {
                throw new ManejadorExcepcion(HttpStatusCode.Conflict,
                    new { mensaje = "No se puede eliminar: el contacto es el contacto primario de una empresa. Primero reasigná el contacto primario de esa empresa." });
            }
            _context.Remove(contacto);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar el contacto." } );
        }
    }
}