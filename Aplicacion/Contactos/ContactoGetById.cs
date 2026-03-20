using System.Net;
using Microsoft.EntityFrameworkCore;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Contactos;

public class ContactoGetById
{
    public class ContactoUnico : IRequest<Dominio.Contacto>
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<ContactoUnico, Dominio.Contacto>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Dominio.Contacto> Handle(ContactoUnico request, CancellationToken cancellationToken)
        {
            var contacto = await _context.Contactos
                .Include(c => c.EstadoCivil)
                .Include(c => c.NivelEstudio)
                .Include(c => c.CategoriaLaboral)
                .Include(c => c.ClienteEmpresa)
                .Include(c => c.SesionesParticipantes)
                    .ThenInclude(sp => sp.Sesion)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
                
            if (contacto == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El contacto no existe o no se ha encontrado."} );
            }
            return contacto;
        }
    }
}