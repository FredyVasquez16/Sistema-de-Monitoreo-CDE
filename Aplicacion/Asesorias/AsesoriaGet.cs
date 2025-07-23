using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaGet
{
    public class ListaAsesorias : IRequest<List<AsesoriaDto>>
    {
    }

    public class Manejador : IRequestHandler<ListaAsesorias, List<AsesoriaDto>>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly IMapper _mapper;
        // UserManager no es necesario para esta consulta, pero lo dejamos por si se usa en el futuro.
        private readonly UserManager<Usuario> _userManager; 
        private readonly IUsuarioSesion _usuarioSesion;

        public Manejador(
            SistemaMonitoreaCdeContext context,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IUsuarioSesion usuarioSesion)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _usuarioSesion = usuarioSesion;
        }

        public async Task<List<AsesoriaDto>> Handle(ListaAsesorias request, CancellationToken cancellationToken)
        {
            var userName = _usuarioSesion.ObtenerUsuarioSesion();

            if (string.IsNullOrEmpty(userName))
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "Sesión no válida." });

            var usuario = await _userManager.FindByNameAsync(userName);
            if (usuario == null)
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized, new { mensaje = "Usuario no encontrado." });

            var usuarioId = usuario.Id; // ← Este es el que está en la tabla usuarios_unidades

            // 2. Obtener las IDs de las Unidades a las que pertenece el asesor.
            // Buscamos en la tabla de unión UsuarioUnidad.
            var unidadesDelAsesorIds = await _context.UsuariosUnidades
                .Where(uu => uu.UsuarioId == usuarioId)
                .Select(ux => ux.UnidadId)
                .ToListAsync(cancellationToken);

            if (!unidadesDelAsesorIds.Any())
            {
                // Si el asesor no está asignado a ninguna unidad, no debería ver ninguna asesoría.
                // Devolvemos una lista vacía.
                return new List<AsesoriaDto>();
            }

            // Filtrar las asesorías basándonos en las unidades del asesor.
            var asesorias = await _context.Asesorias
                .Where(a => a.AsesoriasUnidades.Any(au => unidadesDelAsesorIds.Contains(au.UnidadId)))  // Usamos la tabla intermedia
                .Include(x => x.Asesores)
                .ThenInclude(aa => aa.Asesor)
                .Include(x => x.AsesoriasContactos)
                .ThenInclude(ac => ac.Contacto)
                .Include(x => x.AreaAsesoria)
                .ToListAsync(cancellationToken);

            // 4. Mapear las entidades filtradas al DTO de respuesta.
            var asesoriasDto = _mapper.Map<List<Asesoria>, List<AsesoriaDto>>(asesorias);
            return asesoriasDto;
        }
    }
}