using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaGetById
{
    public class AsesoriaUnica : IRequest<Dominio.Asesorias>
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<AsesoriaUnica, Dominio.Asesorias>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Dominio.Asesorias> Handle(AsesoriaUnica request, CancellationToken cancellationToken)
        {
            var asesoria = await _context.Asesorias.FindAsync(request.Id);
            if (asesoria == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, 
                    new { mensaje = "La asesoría no existe o no se ha encontrado." });
            }
            return asesoria;
        }
    }
}