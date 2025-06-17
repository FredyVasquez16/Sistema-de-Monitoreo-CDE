using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionSubirArchivo
{
    public class Comando : IRequest
    {
        public int CapacitacionId { get; set; }
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
            var capacitacion = await _context.Capacitaciones.FindAsync(request.CapacitacionId);
            if (capacitacion == null)
                throw new Exception("Capacitación no encontrada");
            /*try
            {
                foreach (var archivo in request.Archivos)
                {
                    using var ms = new MemoryStream();
                    await archivo.CopyToAsync(ms, cancellationToken);

                    var nuevoArchivo = new CapacitacionesArchivos
                    {
                        CapacitacionId = request.CapacitacionId,
                        NombreOriginal = archivo.FileName,
                        Contenido = ms.ToArray(),
                        TipoMime = archivo.ContentType,
                        FechaSubida = DateTime.UtcNow
                    };

                    _context.CapacitacionesArchivos.Add(nuevoArchivo);
                }

                var resultado = await _context.SaveChangesAsync(cancellationToken);
                if (resultado <= 0)
                    throw new ManejadorExcepcion(
                        System.Net.HttpStatusCode.InternalServerError,
                        new { mensaje = "No se pudo guardar los archivos" });

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new ManejadorExcepcion(
                    System.Net.HttpStatusCode.InternalServerError,
                    new
                    {
                        mensaje = "Error al guardar en base de datos",
                        detalle = ex.InnerException?.Message ?? ex.Message
                    });
            }*/
            
            foreach (var archivo in request.Archivos)
            {
                using var ms = new MemoryStream();
                await archivo.CopyToAsync(ms, cancellationToken);

                var nuevoArchivo = new CapacitacionesArchivos
                {
                    CapacitacionId = request.CapacitacionId,
                    NombreOriginal = archivo.FileName,
                    Contenido = ms.ToArray(),
                    TipoMime = archivo.ContentType,
                    FechaSubida = DateTime.UtcNow
                };

                _context.CapacitacionesArchivos.Add(nuevoArchivo);
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