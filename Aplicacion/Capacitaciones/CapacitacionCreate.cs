using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionCreate
{
    public class CapacitacionCreateEjecuta : IRequest
    {
        public int TipoId { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaInformes { get; set; }
        public TimeOnly HoraProgramada { get; set; }
        public int? TotalHoras { get; set; }
        public string? Descripcion { get; set; }
        public int TemaPrincipalId { get; set; }
        public int FormatoProgramaId { get; set; }
        public string? Estado { get; set; }
        public int? NumeroMaxParticipantes { get; set; }
        public int NumeroSesiones { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string LugarDesarrollo { get; set; }
        public string? Centro { get; set; }
        public bool PatrociniosCentro { get; set; }
        public string? CoPatrocinios { get; set; }
        public string? Recursos { get; set; }
        public string? Contacto { get; set; }
        public string? CorreoContacto { get; set; }
        public int? TelefonoContacto { get; set; }
        public string? Idioma { get; set; }
        public string? UnidadHistorica { get; set; }
        public int? FuenteFinanciamientoId { get; set; }
        public string? IntruccionesAsistente { get; set; }
        public string? Notas { get; set; }
    }
    
    //Validador para la solicitud de creación de capacitación
    public class EjecutaValidacion : AbstractValidator<CapacitacionCreateEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El título es obligatorio.").MaximumLength(1000).WithMessage("El título no puede exceder los 1000 caracteres.");
            RuleFor(x => x.FechaInicio).NotEmpty().WithMessage("La fecha de inicio es obligatoria.");
            RuleFor(x => x.FechaCierre).NotEmpty().WithMessage("La fecha de cierre es obligatoria.");
            RuleFor(x => x.FechaInformes).NotEmpty().WithMessage("La fecha de informes es obligatoria.");
            RuleFor(x => x.HoraProgramada).NotEmpty().WithMessage("La hora programada es obligatoria.");
            RuleFor(x => x.TemaPrincipalId).GreaterThan(0).WithMessage("El tema principal es obligatorio.");
            RuleFor(x => x.FormatoProgramaId).GreaterThan(0).WithMessage("El formato del programa es obligatorio.");
            RuleFor(x => x.NumeroSesiones).GreaterThan(0).WithMessage("El número de sesiones debe ser mayor que cero.");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("La dirección es obligatoria.").MaximumLength(1000).WithMessage("La dirección no puede exceder los 1000 caracteres.");
            RuleFor(x => x.Ciudad).NotEmpty().WithMessage("La ciudad es obligatoria.").MaximumLength(100).WithMessage("La ciudad no puede exceder los 100 caracteres.");
            RuleFor(x => x.Departamento).NotEmpty().WithMessage("El departamento es obligatorio.").MaximumLength(100).WithMessage("El departamento no puede exceder los 100 caracteres.");
            RuleFor(x => x.LugarDesarrollo).NotEmpty().WithMessage("El lugar de desarrollo es obligatorio.").MaximumLength(200).WithMessage("El lugar de desarrollo no puede exceder los 200 caracteres.");
        }
    }
    
    //Manejador para procesar la solicitud de creación de capacitación
    public class Manejador : IRequestHandler<CapacitacionCreateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CapacitacionCreateEjecuta request, CancellationToken cancellationToken)
        {
            //Validar que el tipo Id exista
            var tipo = await _context.Tipos.FindAsync(request.TipoId);
            if (tipo == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Tipo de capacitación no encontrado" });
            }
            
            //Validar que el tema principal Id exista
            var temaPrincipal = await _context.Temas.FindAsync(request.TemaPrincipalId);
            if (temaPrincipal == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Tema principal no encontrado" });
            }
            
            //Validar que el formato del programa Id exista
            var formatoPrograma = await _context.FormatosProgramas.FindAsync(request.FormatoProgramaId);
            if (formatoPrograma == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Formato del programa no encontrado" });
            }
            //Validar que la fuente de financiamiento Id exista
            if (request.FuenteFinanciamientoId.HasValue)
            {
                var fuenteFinanciamiento = await _context.FuenteFinanciamientos.FindAsync(request.FuenteFinanciamientoId.Value);
                if (fuenteFinanciamiento == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Fuente de financiamiento no encontrada" });
                }
            }
            
            var capacitacion = new Dominio.Capacitaciones
            {
                TipoId = request.TipoId,
                Titulo = request.Titulo,
                FechaInicio = request.FechaInicio,
                FechaCierre = request.FechaCierre,
                FechaInformes = request.FechaInformes,
                HoraProgramada = request.HoraProgramada,
                TotalHoras = request.TotalHoras,
                Descripcion = request.Descripcion,
                TemaPrincipalId = request.TemaPrincipalId,
                FormatoProgramaId = request.FormatoProgramaId,
                Estado = request.Estado,
                NumeroMaxParticipantes = request.NumeroMaxParticipantes,
                NumeroSesiones = request.NumeroSesiones,
                Direccion = request.Direccion,
                Ciudad = request.Ciudad,
                Departamento = request.Departamento,
                LugarDesarrollo = request.LugarDesarrollo,
                Centro = request.Centro,
                PatrociniosCentro = request.PatrociniosCentro,
                CoPatrocinios = request.CoPatrocinios,
                Recursos = request.Recursos,
                Contacto = request.Contacto,
                CorreoContacto = request.CorreoContacto,
                TelefonoContacto = request.TelefonoContacto,
                Idioma = request.Idioma,
                UnidadHistorica = request.UnidadHistorica,
                FuenteFinanciamientoId = request.FuenteFinanciamientoId,
                IntruccionesAsistente = request.IntruccionesAsistente,
                Notas = request.Notas
            };

            _context.Capacitaciones.Add(capacitacion);
            var valor = await _context.SaveChangesAsync();
            if (valor > 0)
            {
                return Unit.Value; // Retorna un valor vacío si la operación fue exitosa
            }
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo crear la capacitación o evento" });
            
            /*try
            {
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                    return Unit.Value;

                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se guardaron los cambios" });
            }
            catch (DbUpdateException ex)
            {
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new
                {
                    mensaje = "Error al guardar en base de datos",
                    detalle = ex.InnerException?.Message ?? ex.Message
                });
            }*/
        }
    }
}