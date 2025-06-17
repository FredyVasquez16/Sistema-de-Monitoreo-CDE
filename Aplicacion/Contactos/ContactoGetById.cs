using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Contactos;

public class ContactoGetById
{
    public class ContactoUnico : IRequest<Dominio.Contactos>
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<ContactoUnico, Dominio.Contactos>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Dominio.Contactos> Handle(ContactoUnico request, CancellationToken cancellationToken)
        {
            var contacto = await _context.Contactos.FindAsync(request.Id);
            if (contacto == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El contacto no existe o no se ha encontrado."} );
            }
            return contacto;
        }
    }
}