using Aplicacion.ClientesEmpresas.DTOs;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class Consulta
{
    public class ListaContactos : IRequest<List<Dominio.Contacto>>
    {
    }
    
    public class Manejador : IRequestHandler<ListaContactos, List<Dominio.Contacto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<List<Dominio.Contacto>> Handle(ListaContactos request, CancellationToken cancellationToken)
        {
            var contactos = await _context.Contactos
                .Include(c => c.EstadoCivil)
                .Include(c => c.CategoriaLaboral)
                .Include(c => c.ClienteEmpresa)
                .Include(c => c.NivelEstudio)
                .Include(c => c.SesionesParticipantes)
                .ToListAsync(cancellationToken);
            return contactos;
        }
    }
    
    public class FiltroContactos : IRequest<List<ContactoDto>>
    {
        public string TerminoBusqueda { get; set; }
    }

    public class ManejadorFiltro : IRequestHandler<FiltroContactos, List<ContactoDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly IMapper _mapper;
        public ManejadorFiltro(SistemaMonitoreaCdeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContactoDto>> Handle(FiltroContactos request, CancellationToken cancellationToken)
        {
            var query = _context.Contactos.AsQueryable();

            if (!string.IsNullOrEmpty(request.TerminoBusqueda))
            {
                // Busca si el término está en el nombre O en el apellido
                query = query.Where(c => 
                    (c.Nombre + " " + c.Apellido).ToLower().Contains(request.TerminoBusqueda.ToLower())
                );
            }
            
            // Limitamos a los primeros 10 resultados para no sobrecargar la UI
            var contactos = await query.Take(10).ToListAsync(cancellationToken);
            
            return _mapper.Map<List<Contacto>, List<ContactoDto>>(contactos);
        }
    }
}