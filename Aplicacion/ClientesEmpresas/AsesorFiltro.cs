using Aplicacion.Asesorias;
using Aplicacion.ClientesEmpresas.DTOs;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class AsesorFiltro
{
    public class AsesorFiltroEjecuta : IRequest<List<AsesorFiltroDto>>
    {
        public string TerminoBusqueda { get; set; }
    }

    public class Manejador : IRequestHandler<AsesorFiltroEjecuta, List<AsesorFiltroDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly UserManager<Usuario> _userManager;

        public Manejador(SistemaMonitoreaCdeContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<AsesorFiltroDto>> Handle(AsesorFiltroEjecuta request, CancellationToken cancellationToken)
        {
            // 1. Obtenemos todos los usuarios que tienen el rol de "Asesor"
            var asesores = await _userManager.GetUsersInRoleAsync("Asesor");

            // 2. Filtramos en memoria por el término de búsqueda
            var asesoresFiltrados = asesores
                .Where(u => string.IsNullOrEmpty(request.TerminoBusqueda) || 
                            u.NombreCompleto.ToLower().Contains(request.TerminoBusqueda.ToLower()))
                .Take(10) // Limitamos los resultados
                .Select(u => new AsesorFiltroDto
                {
                    Id = u.Id,
                    NombreCompleto = u.NombreCompleto
                })
                .ToList();

            return asesoresFiltrados;
        }
    }
}