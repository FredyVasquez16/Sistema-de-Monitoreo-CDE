using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Capacitaciones;

public class CapacitacionUpdate
{
    public class CapacitacionUpdateEjecuta : IRequest
    {
        public int Id { get; set; }
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
    public class EjecutaValidacion : AbstractValidator<CapacitacionUpdateEjecuta>
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
    
    //Manejador para procesar la solicitud de actualización de capacitación
    public class Manejador : IRequestHandler<CapacitacionUpdateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(CapacitacionUpdateEjecuta request, CancellationToken cancellationToken)
        {
            var capacitacion = await _context.Capacitaciones.FindAsync(request.Id);
            if (capacitacion == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Capacitación no encontrada" });
            }
            
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
            
            //Actualizar los campos de la capacitación
            capacitacion.TipoId = request.TipoId != 0 ? request.TipoId : capacitacion.TipoId;
            capacitacion.Titulo = request.Titulo ?? capacitacion.Titulo;
            capacitacion.FechaInicio = request.FechaInicio != default ? request.FechaInicio : capacitacion.FechaInicio;
            capacitacion.FechaCierre = request.FechaCierre != default ? request.FechaCierre : capacitacion.FechaCierre;
            capacitacion.FechaInformes = request.FechaInformes != default ? request.FechaInformes : capacitacion.FechaInformes;
            capacitacion.HoraProgramada = request.HoraProgramada != default ? request.HoraProgramada : capacitacion.HoraProgramada;
            capacitacion.TotalHoras = request.TotalHoras ?? capacitacion.TotalHoras;
            capacitacion.Descripcion = request.Descripcion ?? capacitacion.Descripcion;
            capacitacion.TemaPrincipalId = request.TemaPrincipalId != 0 ? request.TemaPrincipalId : capacitacion.TemaPrincipalId;
            capacitacion.FormatoProgramaId = request.FormatoProgramaId != 0 ? request.FormatoProgramaId : capacitacion.FormatoProgramaId;
            capacitacion.Estado = request.Estado ?? capacitacion.Estado;
            capacitacion.NumeroMaxParticipantes = request.NumeroMaxParticipantes ?? capacitacion.NumeroMaxParticipantes;
            capacitacion.NumeroSesiones = request.NumeroSesiones > 0 ? request.NumeroSesiones : capacitacion.NumeroSesiones;
            capacitacion.Direccion = request.Direccion ?? capacitacion.Direccion;
            capacitacion.Ciudad = request.Ciudad ?? capacitacion.Ciudad;
            capacitacion.Departamento = request.Departamento ?? capacitacion.Departamento;
            capacitacion.LugarDesarrollo = request.LugarDesarrollo ?? capacitacion.LugarDesarrollo;
            capacitacion.Centro = request.Centro ?? capacitacion.Centro;
            capacitacion.PatrociniosCentro = request.PatrociniosCentro;
            capacitacion.CoPatrocinios = request.CoPatrocinios ?? capacitacion.CoPatrocinios;
            capacitacion.Recursos = request.Recursos ?? capacitacion.Recursos;
            capacitacion.Contacto = request.Contacto ?? capacitacion.Contacto;
            capacitacion.CorreoContacto = request.CorreoContacto ?? capacitacion.CorreoContacto;
            capacitacion.TelefonoContacto = request.TelefonoContacto ?? capacitacion.TelefonoContacto;
            capacitacion.Idioma = request.Idioma ?? capacitacion.Idioma;
            capacitacion.UnidadHistorica = request.UnidadHistorica ?? capacitacion.UnidadHistorica;
            capacitacion.FuenteFinanciamientoId = request.FuenteFinanciamientoId ?? capacitacion.FuenteFinanciamientoId;
            capacitacion.IntruccionesAsistente = request.IntruccionesAsistente ?? capacitacion.IntruccionesAsistente;
            capacitacion.Notas = request.Notas ?? capacitacion.Notas;
            
            var resultado = await _context.SaveChangesAsync();
            if (resultado > 0)
            {
                return Unit.Value; // Indica que la operación fue exitosa
            }
            
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new { mensaje = "No se pudo actualizar la capacitación" });
        }
    }
}