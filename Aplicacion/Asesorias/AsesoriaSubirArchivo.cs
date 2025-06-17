using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaSubirArchivo
{
    public class Comando : IRequest
    {
        public int AsesoriaId { get; set; }
        public List<IFormFile> Archivos { get; set; }
    }

    public class Manejador : IRequestHandler<Comando>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Comando request, CancellationToken cancellationToken)
        {
            var asesoria = await _context.Asesorias.FindAsync(request.AsesoriaId);
            if (asesoria == null)
                throw new Exception("Asesoría no encontrada");

            foreach (var archivo in request.Archivos)
            {
                using var ms = new MemoryStream();
                await archivo.CopyToAsync(ms, cancellationToken);

                var nuevoArchivo = new AsesoriasArchivos
                {
                    AsesoriaId = request.AsesoriaId,
                    NombreOriginal = archivo.FileName,
                    Contenido = ms.ToArray(),
                    TipoMime = archivo.ContentType,
                    FechaSubida = DateTime.UtcNow
                };

                _context.AsesoriasArchivos.Add(nuevoArchivo);
            }

            var resultado = await _context.SaveChangesAsync(cancellationToken);
            if (resultado <= 0)
                throw new ManejadorExcepcion(
                    System.Net.HttpStatusCode.InternalServerError,
                    new { mensaje = "No se pudo guardar los archivos" });

            return Unit.Value;
        }
    }
}