using System.Net;
using Microsoft.EntityFrameworkCore;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using Dominio;

namespace Aplicacion.Contactos;

public class ContactoGetAsesorias
{
    public class Query : IRequest<List<ContactoAsesoriaDto>>
    {
        public int ContactoId { get; set; }
    }
    
    public class ContactoAsesoriaDto
    {
        public int AsesoriaId { get; set; }
        public string CodigoUnico { get; set; }
        public string Asunto { get; set; }
        public string AreaAsesoria { get; set; }
        public DateTime FechaSesion { get; set; }
        public int NumeroParticipantes { get; set; }
        public string Notas { get; set; }
    }
    
    public class Manejador : IRequestHandler<Query, List<ContactoAsesoriaDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<List<ContactoAsesoriaDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var asesorias = await _context.AsesoriasContactos
                .Where(ac => ac.ContactoId == request.ContactoId)
                .Include(ac => ac.Asesoria)
                    .ThenInclude(a => a.AreaAsesoria)
                .Select(ac => new ContactoAsesoriaDto
                {
                    AsesoriaId = ac.AsesoriaId,
                    CodigoUnico = ac.Asesoria.CodigoUnico,
                    Asunto = ac.Asesoria.Asunto,
                    AreaAsesoria = ac.Asesoria.AreaAsesoria != null ? ac.Asesoria.AreaAsesoria.Descripcion : "",
                    FechaSesion = ac.Asesoria.FechaSesion,
                    NumeroParticipantes = ac.Asesoria.NumeroParticipantes ?? 0,
                    Notas = ac.Asesoria.Notas
                })
                .ToListAsync();
                
            return asesorias;
        }
    }
}
