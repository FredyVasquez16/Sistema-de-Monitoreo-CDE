using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Contactos;

public class ContactoUpdate
{
    public class ContactoUpdateEjecuta : IRequest
    {
        public int Id { get; set; }
        
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

        public string Ciudad { get; set; }

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
    }
    
    // Validador para la solicitud de actualizacion de contacto
    public class EjecutaValidacion : AbstractValidator<ContactoUpdateEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El campo Nombre es obligatorio.").MaximumLength(100).WithMessage("El campo Nombre no puede exceder los 100 caracteres.");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El campo Apellido es obligatorio.").MaximumLength(100).WithMessage("El campo Apellido no puede exceder los 100 caracteres.");
            RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("El campo Fecha de Nacimiento es obligatorio.");
            RuleFor(x => x.Dni).NotEmpty().WithMessage("El campo DNI es obligatorio.").MaximumLength(20).WithMessage("El campo DNI no puede exceder los 20 caracteres.");
            RuleFor(x => x.Nacionalidad).NotEmpty().WithMessage("El campo Nacionalidad es obligatorio.").MaximumLength(70).WithMessage("El campo Nacionalidad no puede exceder los 70 caracteres.");
            RuleFor(x => x.Genero).NotEmpty().WithMessage("El campo Género es obligatorio.").MaximumLength(50).WithMessage("El campo Género no puede exceder los 50 caracteres.");
            RuleFor(x => x.Telefono).NotEmpty().WithMessage("El campo Teléfono es obligatorio.");
            RuleFor(x => x.Correo).EmailAddress().WithMessage("El campo Correo debe ser un correo electrónico válido.").NotEmpty().WithMessage("El campo Correo es obligatorio.").MaximumLength(100).WithMessage("El campo Correo no puede exceder los 100 caracteres.");
            RuleFor(x => x.Rtn).NotEmpty().WithMessage("El campo RTN es obligatorio.").MaximumLength(20).WithMessage("El campo RTN no puede exceder los 20 caracteres.");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("El campo Dirección es obligatorio.").MaximumLength(1000).WithMessage("El campo Dirección no puede exceder los 1000 caracteres.");
            RuleFor(x => x.Ciudad).NotEmpty().WithMessage("El campo Ciudad es obligatorio.").MaximumLength(100).WithMessage("El campo Ciudad no puede exceder los 100 caracteres.");
            RuleFor(x => x.Departamento).NotEmpty().WithMessage("El campo Departamento es obligatorio.").MaximumLength(100).WithMessage("El campo Departamento no puede exceder los 100 caracteres.");
            RuleFor(x => x.Cargo).NotEmpty().WithMessage("El campo Cargo es obligatorio.").MaximumLength(100).WithMessage("El campo Cargo no puede exceder los 100 caracteres.");
            RuleFor(x => x.NivelEstudioId).GreaterThan(0).WithMessage("Debe seleccionar un Nivel de Estudio válido.");
        }
    }
    
    public class Manejador : IRequestHandler<ContactoUpdateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(ContactoUpdateEjecuta request, CancellationToken cancellationToken)
        {
            var contacto = await _context.Contactos.FindAsync(request.Id);
            if (contacto == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El contacto no existe o no se ha encontrado."} );
            }
            
            //validar si el estado civil id existe
            if (request.EstadoCivilId.HasValue && request.EstadoCivilId.Value > 0)
            {
                var estadoCivil = await _context.EstadosCiviles.FindAsync(request.EstadoCivilId.Value);
                if (estadoCivil == null)
                {
                    //throw new Exception("El Estado Civil especificado no existe.");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El Estado Civil especificado no existe."} );
                }
            }
            
            //validar si el nivel de estudio id existe
            if (request.NivelEstudioId > 0)
            {
                var nivelEstudio = await _context.NivelesEstudios.FindAsync(request.NivelEstudioId);
                if (nivelEstudio == null)
                {
                    //throw new Exception("El Nivel de Estudio especificado no existe.");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "El Nivel de Estudio especificado no existe."} );
                }
            }
            
            //validar si la categoria laboral id existe
            if (request.CategoriaLaboralId.HasValue && request.CategoriaLaboralId.Value > 0)
            {
                var categoriaLaboral = await _context.CategoriasLaborales.FindAsync(request.CategoriaLaboralId.Value);
                if (categoriaLaboral == null)
                {
                    //throw new Exception("La Categoría Laboral especificada no existe.");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new {mensaje = "La Categoría Laboral especificada no existe."} );
                }
            }
            
            contacto.Nombre = request.Nombre ?? contacto.Nombre;
            contacto.Apellido = request.Apellido ?? contacto.Apellido;
            contacto.FechaNacimiento = request.FechaNacimiento != default ? request.FechaNacimiento : contacto.FechaNacimiento;
            contacto.Dni = request.Dni ?? contacto.Dni;
            contacto.Nacionalidad = request.Nacionalidad ?? contacto.Nacionalidad;
            contacto.Genero = request.Genero ?? contacto.Genero;
            contacto.Telefono = request.Telefono != 0 ? request.Telefono : contacto.Telefono;
            contacto.Correo = request.Correo ?? contacto.Correo;
            contacto.Rtn = request.Rtn ?? contacto.Rtn;
            contacto.Direccion = request.Direccion ?? contacto.Direccion;
            contacto.Ciudad = request.Ciudad ?? contacto.Ciudad;
            contacto.Departamento = request.Departamento ?? contacto.Departamento;
            contacto.Cargo = request.Cargo ?? contacto.Cargo;
            contacto.EstadoCivilId = request.EstadoCivilId != 0 ? request.EstadoCivilId : contacto.EstadoCivilId;
            contacto.NivelEstudioId = request.NivelEstudioId != 0 ? request.NivelEstudioId : contacto.NivelEstudioId;
            contacto.CategoriaLaboralId = request.CategoriaLaboralId != 0 ? request.CategoriaLaboralId : contacto.CategoriaLaboralId;
            contacto.PoseeNegocio = request.PoseeNegocio;
            contacto.NombreEtnia = request.NombreEtnia ?? contacto.NombreEtnia;
            contacto.LocalidadEtnica = request.LocalidadEtnica ?? contacto.LocalidadEtnica;
            contacto.ContactoDiscapacidad = request.ContactoDiscapacidad != 0 ? request.ContactoDiscapacidad : contacto.ContactoDiscapacidad;
            contacto.IntegrantesTotalesFamilia = request.IntegrantesTotalesFamilia != 0 ? request.IntegrantesTotalesFamilia : contacto.IntegrantesTotalesFamilia;
            contacto.NumeroHijos = request.NumeroHijos != 0 ? request.NumeroHijos : contacto.NumeroHijos;
            contacto.NumeroHijas = request.NumeroHijas != 0 ? request.NumeroHijas : contacto.NumeroHijas;
            contacto.RolContactoFamiliar = request.RolContactoFamiliar ?? contacto.RolContactoFamiliar;
            contacto.Centro = request.Centro ?? contacto.Centro;
            contacto.Notas = request.Notas ?? contacto.Notas;

            var resultado = await _context.SaveChangesAsync();
            if (resultado > 0)
            {
                return Unit.Value; // Indica que la operación fue exitosa
            }
            
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError, new {mensaje = "No se pudo actualizar el contacto."} );
        }
    }
}