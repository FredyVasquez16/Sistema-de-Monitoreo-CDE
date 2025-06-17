using System.Net;
using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionDelete
{
    public class CapacitacionDeleteEjecuta : IRequest
    {
        public int Id { get; set; }
    }

    public class Manejador : IRequestHandler<CapacitacionDeleteEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CapacitacionDeleteEjecuta request, CancellationToken cancellationToken)
        {
            var capacitacion = await _context.Capacitaciones.FindAsync(request.Id);
            if (capacitacion == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "La capacitación no existe o no se ha encontrado." });
            }
            _context.Remove(capacitacion);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Unit.Value;
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo eliminar la capacitación." });
        }
    }
}