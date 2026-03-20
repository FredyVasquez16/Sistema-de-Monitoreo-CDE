using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ClientesEmpresas;

public class NivelFormalizacionGet
{
    public class ListaNivelFormalizacion : IRequest<List<Dominio.NivelesFormalizacion>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaNivelFormalizacion, List<Dominio.NivelesFormalizacion>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        
        public Manejador(Persistencia.SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.NivelesFormalizacion>> Handle(ListaNivelFormalizacion request, CancellationToken cancellationToken)
        {
            var nivelesFormalizacion = await _context.NivelesFormalizaciones.ToListAsync();
            return nivelesFormalizacion;
        }
    }
}