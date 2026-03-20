using System.ComponentModel.DataAnnotations;
using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Contactos;

public class ContactoCreate
{
    // Clase que representa la solicitud de creación de un contacto
    public class ContactoCreateEjecuta : IRequest
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Dni { get; set; }

        public string Nacionalidad { get; set; }

        public string Genero { get; set; }

        public int Telefono { get; set; }

        public string Correo { get; set; }

        public string Rtn { get; set; }

        public string Direccion { get; set; }

        public string Municipio { get; set; }

        public string Departamento { get; set; }

        public string Cargo { get; set; }

        public int? EstadoCivilId { get; set; }

        public int NivelEstudioId { get; set; }

        public int? CategoriaLaboralId { get; set; }

        public bool PoseeNegocio { get; set; }

        public string? NombreEtnia { get; set; }

        public string? LocalidadEtnica { get; set; }

        public int? ContactoDiscapacidad { get; set; }

        public int? IntegrantesTotalesFamilia { get; set; }

        public int? NumeroHijos { get; set; }

        public int? NumeroHijas { get; set; }

        public string? RolContactoFamiliar { get; set; }

        public string? Centro { get; set; }

        public string? Notas { get; set; }

        //public int? ClienteEmpresaId { get; set; }
    }

    // Validador para la solicitud de creación de contacto
    public class EjecutaValidacion : AbstractValidator<ContactoCreateEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El campo Nombre es obligatorio.").MaximumLength(100)
                .WithMessage("El campo Nombre no puede exceder los 100 caracteres.");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El campo Apellido es obligatorio.").MaximumLength(100)
                .WithMessage("El campo Apellido no puede exceder los 100 caracteres.");
            RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("El campo Fecha de Nacimiento es obligatorio.");
            RuleFor(x => x.Dni).NotEmpty().WithMessage("El campo DNI es obligatorio.").MaximumLength(20)
                .WithMessage("El campo DNI no puede exceder los 20 caracteres.");
            RuleFor(x => x.Nacionalidad).NotEmpty().WithMessage("El campo Nacionalidad es obligatorio.")
                .MaximumLength(70).WithMessage("El campo Nacionalidad no puede exceder los 70 caracteres.");
            RuleFor(x => x.Genero).NotEmpty().WithMessage("El campo Género es obligatorio.").MaximumLength(50)
                .WithMessage("El campo Género no puede exceder los 50 caracteres.");
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("El campo Teléfono es obligatorio.");
            RuleFor(x => x.Correo).EmailAddress().WithMessage("El campo Correo debe ser un correo electrónico válido.")
                .NotEmpty().WithMessage("El campo Correo es obligatorio.").MaximumLength(100)
                .WithMessage("El campo Correo no puede exceder los 100 caracteres.");
            RuleFor(x => x.Rtn).NotEmpty().WithMessage("El campo RTN es obligatorio.").MaximumLength(20)
                .WithMessage("El campo RTN no puede exceder los 20 caracteres.");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("El campo Dirección es obligatorio.").MaximumLength(1000)
                .WithMessage("El campo Dirección no puede exceder los 1000 caracteres.");
            RuleFor(x => x.Municipio).NotEmpty().WithMessage("El campo Municipio es obligatorio.").MaximumLength(100)
                .WithMessage("El campo Municipio no puede exceder los 100 caracteres.");
            RuleFor(x => x.Departamento).NotEmpty().WithMessage("El campo Departamento es obligatorio.")
                .MaximumLength(100).WithMessage("El campo Departamento no puede exceder los 100 caracteres.");
            RuleFor(x => x.Cargo).NotEmpty().WithMessage("El campo Cargo es obligatorio.").MaximumLength(100)
                .WithMessage("El campo Cargo no puede exceder los 100 caracteres.");
            RuleFor(x => x.NivelEstudioId).GreaterThan(0).WithMessage("Debe seleccionar un Nivel de Estudio válido.");
        }
    }

    // Manejador para procesar la solicitud de creación de contacto
    public class Manejador : IRequestHandler<ContactoCreateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;

        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ContactoCreateEjecuta request, CancellationToken cancellationToken)
        {
            // === PASO 1: VALIDACIONES (igual que antes) ===
            if (request.EstadoCivilId.HasValue &&
                !await _context.EstadosCiviles.AnyAsync(x => x.Id == request.EstadoCivilId.Value, cancellationToken))
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El Estado Civil no existe" });
            }

            if (!await _context.NivelesEstudios.AnyAsync(x => x.Id == request.NivelEstudioId, cancellationToken))
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "El Nivel de Estudio no existe" });
            }

            if (request.CategoriaLaboralId.HasValue &&
                !await _context.CategoriasLaborales.AnyAsync(x => x.Id == request.CategoriaLaboralId.Value,
                    cancellationToken))
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "La Categoria Laboral no existe" });
            }

            // === PASO 2: GENERACIÓN DEL CÓDIGO ÚNICO ===
            var ultimoCodigo = await _context.Contactos
                .Where(c => c.CodigoUnico.StartsWith("CDE-CO-"))
                .OrderByDescending(c => c.Id) // Ordenamos por ID para asegurar el último insertado
                .Select(c => c.CodigoUnico)
                .FirstOrDefaultAsync(cancellationToken);

            int nuevoNumero = 1;
            if (!string.IsNullOrEmpty(ultimoCodigo))
            {
                var partes = ultimoCodigo.Split('-');
                if (partes.Length > 1 && int.TryParse(partes.Last(), out int ultimoNumero))
                {
                    nuevoNumero = ultimoNumero + 1;
                }
            }

            var nuevoCodigoUnico = $"CDE-CO-{nuevoNumero:D5}"; // D5 para tener ceros a la izquierda, ej: CO-00001

            // === PASO 3: CREACIÓN DEL OBJETO CONTACTO ===
            var contacto = new Dominio.Contacto()
            {
                // Asignamos el código generado ANTES de guardar
                CodigoUnico = nuevoCodigoUnico,

                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = DateTime.SpecifyKind(request.FechaNacimiento, DateTimeKind.Utc),
                Dni = request.Dni,
                Nacionalidad = request.Nacionalidad,
                Genero = request.Genero,
                Telefono = request.Telefono,
                Correo = request.Correo,
                Rtn = request.Rtn,
                Direccion = request.Direccion,
                Municipio = request.Municipio,
                Departamento = request.Departamento,
                Cargo = request.Cargo,
                EstadoCivilId = request.EstadoCivilId,
                NivelEstudioId = request.NivelEstudioId,
                CategoriaLaboralId = request.CategoriaLaboralId,
                PoseeNegocio = request.PoseeNegocio,
                NombreEtnia = request.NombreEtnia,
                LocalidadEtnica = request.LocalidadEtnica,
                ContactoDiscapacidad = request.ContactoDiscapacidad,
                IntegrantesTotalesFamilia = request.IntegrantesTotalesFamilia,
                NumeroHijos = request.NumeroHijos,
                NumeroHijas = request.NumeroHijas,
                RolContactoFamiliar = request.RolContactoFamiliar,
                Centro = request.Centro,
                Notas = request.Notas
            };

            // === PASO 4: GUARDADO ===
            _context.Contactos.Add(contacto);
            var valor = await _context.SaveChangesAsync(cancellationToken);

            if (valor > 0)
            {
                return Unit.Value; // ¡Éxito!
            }

            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                new { mensaje = "No se pudo insertar el contacto" });
        }
    }
}