using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaCreate
{
    public class AsesoriaCreateEjecuta : IRequest
    {
        public int ClienteId { get; set; }
        public DateTime FechaSesion { get; set; }
        public TimeOnly? TiempoContacto { get; set; }
        public int TipoContactoId { get; set; }
        public int AreaAsesoriaId { get; set; }
        public string? AyudaAdicional { get; set; }
        public string? Asunto { get; set; }
        public int FuenteFinanciamientoId { get; set; }
        public string? Centro { get; set; }
        public int? NumeroParticipantes { get; set; }
        public string? Notas { get; set; }
        public string? ReferidoA { get; set; }
        public string? DescripcionReferido { get; set; }
        public string? DescripcionDerivado { get; set; }
        public string? DescripcionAsesoriaEspecializada { get; set; }
    }

    //validador para la solicitud de creación de una asesoría
    public class EjecutaValidacion : AbstractValidator<AsesoriaCreateEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.ClienteId).GreaterThan(0).WithMessage("El Cliente es obligatorio.");
            RuleFor(x => x.FechaSesion).NotEmpty().WithMessage("La Fecha de Sesión es obligatoria.");
            RuleFor(x => x.TipoContactoId).GreaterThan(0).WithMessage("El Tipo de Contacto es obligatorio.");
            RuleFor(x => x.AreaAsesoriaId).GreaterThan(0).WithMessage("El Área de Asesoría es obligatoria.");
            RuleFor(x => x.FuenteFinanciamientoId).GreaterThan(0)
                .WithMessage("La Fuente de Financiamiento es obligatoria.");
        }
    }

    public class Manejador : IRequestHandler<AsesoriaCreateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AsesoriaCreateEjecuta request, CancellationToken cancellationToken)
        {
            // Validar si el cliente existe
            var cliente = await _context.ClientesEmpresas.FindAsync(request.ClienteId);
            if (cliente == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"El cliente con el Id {request.ClienteId} no fue encontrado." });
            }
            // Validar si el tipo de contacto existe
            var tipoContacto = await _context.TiposContactos.FindAsync(request.TipoContactoId);
            if (tipoContacto == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"El tipo de contacto con el Id {request.TipoContactoId} no fue encontrado." });
            }
            // Validar si el área de asesoría existe
            var areaAsesoria = await _context.AreasAsesorias.FindAsync(request.AreaAsesoriaId);
            if (areaAsesoria == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"El área de asesoría con el Id {request.AreaAsesoriaId} no fue encontrada." });
            }
            // Validar si la fuente de financiamiento existe
            var fuenteFinanciamiento = await _context.FuenteFinanciamientos.FindAsync(request.FuenteFinanciamientoId);
            if (fuenteFinanciamiento == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"La fuente de financiamiento con el Id {request.FuenteFinanciamientoId} no fue encontrada." });
            }
            
            var asesoria = new Dominio.Asesorias
            {
                ClienteId = request.ClienteId,
                FechaSesion = request.FechaSesion,
                TiempoContacto = request.TiempoContacto,
                TipoContactoId = request.TipoContactoId,
                AreaAsesoriaId = request.AreaAsesoriaId,
                AyudaAdicional = request.AyudaAdicional,
                Asunto = request.Asunto,
                FuenteFinanciamientoId = request.FuenteFinanciamientoId,
                Centro = request.Centro,
                NumeroParticipantes = request.NumeroParticipantes,
                Notas = request.Notas,
                ReferidoA = request.ReferidoA,
                DescripcionReferido = request.DescripcionReferido,
                DescripcionDerivado = request.DescripcionDerivado,
                DescripcionAsesoriaEspecializada = request.DescripcionAsesoriaEspecializada
            };

            _context.Asesorias.Add(asesoria);
            var valor = await _context.SaveChangesAsync();
            if (valor > 0)
            {
                return Unit.Value; //Retorna un valor vacío si la creación fue exitosa
            }

            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                new { mensaje = "No se pudo crear la asesoría" });
        }
    }
}