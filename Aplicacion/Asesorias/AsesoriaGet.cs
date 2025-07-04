using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Asesorias;

public class AsesoriaGet
{
    public class ListaAsesorias : IRequest<List<AsesoriaDto>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaAsesorias, List<AsesoriaDto>>
    {
        private readonly Persistencia.SistemaMonitoreaCdeContext _context;
        private readonly IMapper _mapper;

        public Manejador(Persistencia.SistemaMonitoreaCdeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AsesoriaDto>> Handle(ListaAsesorias request, CancellationToken cancellationToken)
        {
            var asesorias = await _context.Asesorias
                .Include(x => x.Asesores)
                .ThenInclude(a => a.Asesor)
                .Include(x => x.AsesoriasContactos)
                    .ThenInclude(x => x.Contacto)
                .ToListAsync(cancellationToken);
            var asesoriasDto = _mapper.Map<List<Asesoria>, List<AsesoriaDto>>(asesorias);
            return asesoriasDto;
        }
    }
}