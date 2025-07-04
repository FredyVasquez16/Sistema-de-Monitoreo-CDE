using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaGetById
{
    public class AsesoriaUnica : IRequest<AsesoriaDto>
    {
        public int Id { get; set; }
    }
    
    public class Manejador : IRequestHandler<AsesoriaUnica, AsesoriaDto>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly IMapper _mapper;

        public Manejador(SistemaMonitoreaCdeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AsesoriaDto> Handle(AsesoriaUnica request, CancellationToken cancellationToken)
        {
            var asesoria = await _context.Asesorias.Include(x => x.Asesores)
                .ThenInclude(y => y.Asesor)
                .Include(x => x.AsesoriasContactos)
                .ThenInclude(x => x.Contacto)
                .FirstOrDefaultAsync(a => a.Id == request.Id);
            
            if (asesoria == null)
            {
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, 
                    new { mensaje = "La asesoría no existe o no se ha encontrado." });
            }
            
            var asesoriaDto = _mapper.Map<Asesoria, AsesoriaDto>(asesoria);
            return asesoriaDto;
        }
    }
}