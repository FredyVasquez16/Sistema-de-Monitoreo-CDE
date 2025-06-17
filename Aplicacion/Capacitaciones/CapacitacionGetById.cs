using MediatR;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionGetById
{
    public class CapacitacionUnica : IRequest<Dominio.Capacitaciones>
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<CapacitacionUnica, Dominio.Capacitaciones>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Dominio.Capacitaciones> Handle(CapacitacionUnica request, CancellationToken cancellationToken)
        {
            var capacitacion = await _context.Capacitaciones.FindAsync(request.Id);
            if (capacitacion == null)
            {
                throw new Aplicacion.ManejadorError.ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, 
                    new { mensaje = "La capacitación no existe o no se ha encontrado." });
            }
            return capacitacion;
        }
    }
}