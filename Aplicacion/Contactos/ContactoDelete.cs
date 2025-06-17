using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
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