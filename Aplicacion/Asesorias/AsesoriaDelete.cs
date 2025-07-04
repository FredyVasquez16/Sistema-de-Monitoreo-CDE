using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaDelete
{
    public class AsesoriaDeleteEjecuta : IRequest
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<AsesoriaDeleteEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AsesoriaDeleteEjecuta request, CancellationToken cancellationToken)
        {
            var asesoria = await _context.Asesorias.FindAsync(request.Id);
            if (asesoria == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La asesoría no existe o no se ha encontrado." });
            }
            
            var asesoresDB = _context.AsesoriasAsesores.Where(x => x.Id == request.Id);
            foreach (var asesor in asesoresDB)
            {
                _context.Remove(asesor);
            }
            
            var contactosDB = _context.AsesoriasContactos.Where(x => x.Id == request.Id);
            foreach (var contacto in contactosDB)
            {
                _context.Remove(contacto);
            }
            
            var archivosDB = _context.AsesoriasArchivos.Where(x => x.Id == request.Id);
            foreach (var archivo in archivosDB)
            {
                _context.Remove(archivo);
            }
            
            _context.Remove(asesoria);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar la asesoría." });
        }
    }
}