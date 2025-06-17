using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class Consulta
{
    public class ListaContactos : IRequest<List<Dominio.Contactos>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaContactos, List<Dominio.Contactos>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<List<Dominio.Contactos>> Handle(ListaContactos request, CancellationToken cancellationToken)
        {
            var contactos = await _context.Contactos.ToListAsync();
            return contactos;
        }
    }
}