using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class NivelEstudioGet
{
    public class NivelEstudioGetEjecuta : IRequest<List<Dominio.NivelesEstudio>>
    {
        
    }

    public class Manejador : IRequestHandler<NivelEstudioGetEjecuta, List<Dominio.NivelesEstudio>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.NivelesEstudio>> Handle(NivelEstudioGetEjecuta request, CancellationToken cancellationToken)
        {
            var nivelesEstudios = await _context.NivelesEstudios.ToListAsync();
            return nivelesEstudios;
        }
    }
}