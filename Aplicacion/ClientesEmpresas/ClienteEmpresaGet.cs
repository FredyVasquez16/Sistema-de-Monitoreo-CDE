using Aplicacion.ClientesEmpresas.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaGet
{
    // La solicitud ahora devolverá una lista de DTOs
    public class ListaClientesEmpresas : IRequest<List<ClienteEmpresaDto>>
    {
    }

    // El manejador también se actualiza para trabajar con el DTO
    public class Manejador : IRequestHandler<ListaClientesEmpresas, List<ClienteEmpresaDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly IMapper _mapper; // Inyectamos AutoMapper

        public Manejador(SistemaMonitoreaCdeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClienteEmpresaDto>> Handle(ListaClientesEmpresas request, CancellationToken cancellationToken)
        {
            var clientesEmpresas = await _context.ClientesEmpresas
                // Usamos Include para cargar las entidades relacionadas en una sola consulta
                .Include(ce => ce.ContactoPrimario)
                .Include(ce => ce.Usuario) // 'Usuario' es la propiedad de navegación para el asesor
                .ToListAsync(cancellationToken);

            // Usamos AutoMapper para transformar la lista de entidades a una lista de DTOs
            var clientesEmpresasDto = _mapper.Map<List<Dominio.ClientesEmpresas>, List<ClienteEmpresaDto>>(clientesEmpresas);
                
            return clientesEmpresasDto;
        }
    }
}
