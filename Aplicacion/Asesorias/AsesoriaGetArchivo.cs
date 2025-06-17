using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaGetArchivo
{
    public class Query : IRequest<List<ArchivoDto>>
    {
        public int AsesoriaId { get; set; }
    }

    public class ArchivoDto
    {
        public int Id { get; set; }
        public string NombreOriginal { get; set; }
        public string TipoMime { get; set; }
        public DateTime FechaSubida { get; set; }
    }

    public class Manejador : IRequestHandler<Query, List<ArchivoDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<ArchivoDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var archivos = await _context.AsesoriasArchivos
                .Where(a => a.AsesoriaId == request.AsesoriaId)
                .Select(a => new ArchivoDto
                {
                    Id = a.Id,
                    NombreOriginal = a.NombreOriginal,
                    TipoMime = a.TipoMime,
                    FechaSubida = a.FechaSubida
                })
                .ToListAsync(cancellationToken);

            return archivos;
        }
    }
}