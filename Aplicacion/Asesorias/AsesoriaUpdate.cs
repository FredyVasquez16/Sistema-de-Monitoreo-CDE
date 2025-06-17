using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Asesorias;

public class AsesoriaUpdate
{
    public class AsesoriaUpdateEjecuta : IRequest
    {
        public int Id { get; set; }
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
    public class EjecutaValidacion : AbstractValidator<AsesoriaUpdateEjecuta>
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
    
    public class Manejador : IRequestHandler<AsesoriaUpdateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AsesoriaUpdateEjecuta request, CancellationToken cancellationToken)
        {
            //Validar si la asesoría existe
            var asesoria = await _context.Asesorias.FindAsync(request.Id);
            if (asesoria == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"La asesoría con el Id {request.Id} no fue encontrada." });
            }
            
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
            
            // Actualizar los campos de la asesoría
            asesoria.ClienteId = request.ClienteId != 0 ? request.ClienteId : asesoria.ClienteId;
            asesoria.FechaSesion = request.FechaSesion != default ? request.FechaSesion : asesoria.FechaSesion;
            asesoria.TiempoContacto = request.TiempoContacto ?? asesoria.TiempoContacto;
            asesoria.TipoContactoId = request.TipoContactoId != 0 ? request.TipoContactoId : asesoria.TipoContactoId;
            asesoria.AreaAsesoriaId = request.AreaAsesoriaId != 0 ? request.AreaAsesoriaId : asesoria.AreaAsesoriaId;
            asesoria.AyudaAdicional = request.AyudaAdicional ?? asesoria.AyudaAdicional;
            asesoria.Asunto = request.Asunto ?? asesoria.Asunto;
            asesoria.FuenteFinanciamientoId = request.FuenteFinanciamientoId != 0 ? request.FuenteFinanciamientoId : asesoria.FuenteFinanciamientoId;
            asesoria.Centro = request.Centro ?? asesoria.Centro;
            asesoria.NumeroParticipantes = request.NumeroParticipantes ?? asesoria.NumeroParticipantes;
            asesoria.Notas = request.Notas ?? asesoria.Notas;
            asesoria.ReferidoA = request.ReferidoA ?? asesoria.ReferidoA;
            asesoria.DescripcionReferido = request.DescripcionReferido ?? asesoria.DescripcionReferido;
            asesoria.DescripcionDerivado = request.DescripcionDerivado ?? asesoria.DescripcionDerivado;
            asesoria.DescripcionAsesoriaEspecializada = request.DescripcionAsesoriaEspecializada ?? asesoria.DescripcionAsesoriaEspecializada;
            
            // Guardar los cambios en la base de datos
            var resultado = await _context.SaveChangesAsync();
            if (resultado > 0)
            {
                return Unit.Value; // Retorna un valor vacío si la actualización fue exitosa
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                new { mensaje = "No se pudo actualizar la asesoría" });
        }
    }
}