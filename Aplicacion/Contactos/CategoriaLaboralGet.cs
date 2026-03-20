using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class CategoriaLaboralGet
{
    public class CategoriaLaboralGetEjecuta : IRequest<List<Dominio.CategoriasLaborales>>
    {
        
    }

    public class Manejador : IRequestHandler<CategoriaLaboralGetEjecuta, List<Dominio.CategoriasLaborales>>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<List<Dominio.CategoriasLaborales>> Handle(CategoriaLaboralGetEjecuta request, CancellationToken cancellationToken)
        {
            var categoriasLaborales = await _context.CategoriasLaborales.ToListAsync();
            return categoriasLaborales;
        }
    }
}