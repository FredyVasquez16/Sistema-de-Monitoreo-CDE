using System.Net;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaUpdate
{
    public class ClienteEmpresaUpdateEjecuta : IRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoClienteNivelId { get; set; }
        public int ContactoPrimarioId { get; set; }
        public int TipoClienteEstadoId { get; set; }
        public int UsuarioId { get; set; }
        public int ServicioSolicitadoId { get; set; }
        public string? RazonSocial { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string? PaginaWeb { get; set; }
        public DateTime FechaInicio { get; set; }
        public string DireccionFisica { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public int TipoOrganizacionId { get; set; }
        public int TipoEmpresaId { get; set; }
        public int TamanoEmpresaId { get; set; }
        public int TipoContabilidadId { get; set; }
        public int NivelFormalizacionId { get; set; }
        public bool ParticipaGremio { get; set; }
        public bool BeneficiadoCde { get; set; }
        public string? TipoCasoEnProceso { get; set; }
        public int EmpleadosTiempoCompleto { get; set; }
        public int? EmpleadosMedioTiempo { get; set; }
        public int? TrabajadoresInformales { get; set; }
        public bool NegocioEnLinea { get; set; }
        public bool NegocioEnCasa { get; set; }
        public int? ComercioInternacionalId { get; set; }
        public string? PaisExporta { get; set; }
        public bool ContratoGobierno { get; set; }
        public bool ZonaIndigena { get; set; }
        public int FuenteFinanciamientoId { get; set; }
        public int? SubFuenteFinanciamientoId { get; set; }
        public double IngresosBrutosAnuales { get; set; }
        public DateTime FechaIngresosBrutos { get; set; }
        public double? IngresosExportaciones { get; set; }
        public double? GananciasPerdidasBrutas { get; set; }
        public DateTime FechaGananciasPerdidasBrutas { get; set; }
        public string DescripcionProductoServicio { get; set; }
        public string? AreasADominar { get; set; }
        public string? Instrucciones { get; set; }
        public string? Motivacion { get; set; }
        public string? LugarDesarrolloEmprendimiento { get; set; }
        public string? Obstaculos { get; set; }
        public string FondoConcursable { get; set; }
        public string EstatusInicial { get; set; }
        public string EstatusActual { get; set; }
        public DateTime FechaEstablecimiento { get; set; }
        public int NombrePropietarioId { get; set; }
        public string GeneroPropietario { get; set; }
        public bool HaSolicitadoCredito { get; set; }
        public string? ComoSolicitoCredito { get; set; }
        public string? PorQueNoCredito { get; set; }
        public bool UsaPagoElectronico { get; set; }
        public string? MediosPago { get; set; }
        public string? Notas { get; set; }
    }
    
    //Validador para la solicitud de actualización de cliente empresa
    public class EjecutaValidacion : AbstractValidator<ClienteEmpresaUpdateEjecuta>
    {
        public EjecutaValidacion()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.").MaximumLength(100)
                .WithMessage("El nombre no puede exceder los 100 caracteres.");
            RuleFor(x => x.TipoClienteNivelId).GreaterThan(0)
                .WithMessage("El nivel del tipo de cliente es obligatorio.");
            RuleFor(x => x.ContactoPrimarioId).GreaterThan(0).WithMessage("El contacto primario es obligatorio.");
            RuleFor(x => x.TipoClienteEstadoId).GreaterThan(0)
                .WithMessage("El estado del tipo de cliente es obligatorio.");
            RuleFor(x => x.UsuarioId).GreaterThan(0).WithMessage("El usuario es obligatorio.");
            RuleFor(x => x.ServicioSolicitadoId).GreaterThan(0).WithMessage("El servicio solicitado es obligatorio.");
            RuleFor(x => x.Telefono).GreaterThan(0).WithMessage("El teléfono es obligatorio.").GreaterThan(7)
                .WithMessage("El teléfono debe tener al menos 8 dígitos.");
            RuleFor(x => x.Correo).EmailAddress().WithMessage("El correo electrónico no es válido.").MaximumLength(100)
                .WithMessage("El correo electrónico no puede exceder los 100 caracteres.");
            RuleFor(x => x.FechaInicio).NotEmpty().WithMessage("La fecha de inicio es obligatoria.");
            RuleFor(x => x.DireccionFisica).NotEmpty().WithMessage("La dirección física es obligatoria.")
                .MaximumLength(1000).WithMessage("La dirección física no puede exceder los 1000 caracteres.");
            RuleFor(x => x.Ciudad).NotEmpty().WithMessage("La ciudad es obligatoria.").MaximumLength(100)
                .WithMessage("La ciudad no puede exceder los 100 caracteres.");
            RuleFor(x => x.Departamento).NotEmpty().WithMessage("El departamento es obligatorio.").MaximumLength(100)
                .WithMessage("El departamento no puede exceder los 100 caracteres.");
            RuleFor(x => x.TipoOrganizacionId).GreaterThan(0).WithMessage("El tipo de organización es obligatorio.");
            RuleFor(x => x.TipoEmpresaId).GreaterThan(0).WithMessage("El tipo de empresa es obligatorio.");
            RuleFor(x => x.TamanoEmpresaId).GreaterThan(0).WithMessage("El tamaño de la empresa es obligatorio.");
            RuleFor(x => x.TipoContabilidadId).GreaterThan(0).WithMessage("El tipo de contabilidad es obligatorio.");
            RuleFor(x => x.NivelFormalizacionId).GreaterThan(0)
                .WithMessage("El nivel de formalización es obligatorio.");
            RuleFor(x => x.EmpleadosTiempoCompleto).GreaterThan(0)
                .WithMessage("El número de empleados a tiempo completo es obligatorio.");
            RuleFor(x => x.IngresosBrutosAnuales).GreaterThan(0)
                .WithMessage("Los ingresos brutos anuales son obligatorios.");
            RuleFor(x => x.DescripcionProductoServicio).NotEmpty()
                .WithMessage("La descripción del producto o servicio es obligatoria.").MaximumLength(2000)
                .WithMessage("La descripción del producto o servicio no puede exceder los 2000 caracteres.");
            RuleFor(x => x.FondoConcursable).NotEmpty().WithMessage("El fondo concursable es obligatorio.")
                .MaximumLength(100).WithMessage("El fondo concursable no puede exceder los 100 caracteres.");
            RuleFor(x => x.EstatusInicial).NotEmpty().WithMessage("El estatus inicial es obligatorio.")
                .MaximumLength(200).WithMessage("El estatus inicial no puede exceder los 200 caracteres.");
            RuleFor(x => x.EstatusActual).NotEmpty().WithMessage("El estatus actual es obligatorio.").MaximumLength(200)
                .WithMessage("El estatus actual no puede exceder los 200 caracteres.");
            RuleFor(x => x.NombrePropietarioId).GreaterThan(0).WithMessage("El nombre del propietario es obligatorio.");
            RuleFor(x => x.GeneroPropietario).NotEmpty().WithMessage("El género del propietario es obligatorio.")
                .MaximumLength(100).WithMessage("El género del propietario no puede exceder los 100 caracteres.");
        }
    }

    public class Manejador : IRequestHandler<ClienteEmpresaUpdateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        public Manejador(SistemaMonitoreaCdeContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ClienteEmpresaUpdateEjecuta request, CancellationToken cancellationToken)
        {
            var clienteEmpresa = await _context.ClientesEmpresas.FindAsync(request.Id);
            if (clienteEmpresa == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Cliente o empresa no encontrado" });
            }
            
            // Validar si el tipo de cliente nivel existe
            var tipoClienteNivel = await _context.TiposClienteNiveles.FindAsync(request.TipoClienteNivelId);
            if (tipoClienteNivel == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tipo de cliente nivel no encontrado" });
            }

            // Validar si el contacto primario existe
            var contactoPrimario = await _context.Contactos.FindAsync(request.ContactoPrimarioId);
            if (contactoPrimario == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Contacto primario no encontrado" });
            }

            // Validar si el tipo de cliente estado existe
            var tipoClienteEstado = await _context.TiposClientesEstados.FindAsync(request.TipoClienteEstadoId);
            if (tipoClienteEstado == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tipo de cliente estado no encontrado" });
            }

            // Validar si el servicio solicitado existe
            var servicioSolicitado = await _context.ServiciosSolicitados.FindAsync(request.ServicioSolicitadoId);
            if (servicioSolicitado == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Servicio solicitado no encontrado" });
            }

            // Validar si el tipo de organización existe
            var tipoOrganizacion = await _context.TiposOrganizaciones.FindAsync(request.TipoOrganizacionId);
            if (tipoOrganizacion == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tipo de organización no encontrado" });
            }

            // Validar si el tipo de empresa existe
            var tipoEmpresa = await _context.TiposEmpresas.FindAsync(request.TipoEmpresaId);
            if (tipoEmpresa == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tipo de empresa no encontrado" });
            }

            // Validar si el tamaño de la empresa existe
            var tamanoEmpresa = await _context.TamanoEmpresas.FindAsync(request.TamanoEmpresaId);
            if (tamanoEmpresa == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tamaño de empresa no encontrado" });
            }

            // Validar si el tipo de contabilidad existe
            var tipoContabilidad = await _context.TiposContabilidades.FindAsync(request.TipoContabilidadId);
            if (tipoContabilidad == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Tipo de contabilidad no encontrado" });
            }

            // Validar si el nivel de formalización existe
            var nivelFormalizacion = await _context.NivelesFormalizaciones.FindAsync(request.NivelFormalizacionId);
            if (nivelFormalizacion == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Nivel de formalización no encontrado" });
            }

            // Validar si el comercio internacional existe
            if (request.ComercioInternacionalId.HasValue)
            {
                var comercioInternacional =
                    await _context.TiposComerciosInternacionales.FindAsync(request.ComercioInternacionalId.Value);
                if (comercioInternacional == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new { mensaje = "Comercio internacional no encontrado" });
                }
            }

            // Validar si la fuente de financiamiento existe
            var fuenteFinanciamiento = await _context.FuenteFinanciamientos.FindAsync(request.FuenteFinanciamientoId);
            if (fuenteFinanciamiento == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Fuente de financiamiento no encontrada" });
            }

            // Validar si la sub fuente de financiamiento existe
            if (request.SubFuenteFinanciamientoId.HasValue)
            {
                var subFuenteFinanciamiento =
                    await _context.FuenteFinanciamientos.FindAsync(request.SubFuenteFinanciamientoId.Value);
                if (subFuenteFinanciamiento == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new { mensaje = "Sub fuente de financiamiento no encontrada" });
                }
            }

            // Validar si el nombre del propietario existe
            var nombrePropietario = await _context.Contactos.FindAsync(request.NombrePropietarioId);
            if (nombrePropietario == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = "Nombre del propietario no encontrado" });
            }
            
            // Actualizar los campos del cliente empresa
            clienteEmpresa.Nombre = request.Nombre ?? clienteEmpresa.Nombre;
            clienteEmpresa.TipoClienteNivelId = request.TipoClienteNivelId != 0 ? request.TipoClienteNivelId : clienteEmpresa.TipoClienteNivelId;
            clienteEmpresa.ContactoPrimarioId = request.ContactoPrimarioId != 0 ? request.ContactoPrimarioId : clienteEmpresa.ContactoPrimarioId;
            clienteEmpresa.TipoClienteEstadoId = request.TipoClienteEstadoId != 0 ? request.TipoClienteEstadoId : clienteEmpresa.TipoClienteEstadoId;
            clienteEmpresa.UsuarioId = request.UsuarioId != 0 ? request.UsuarioId : clienteEmpresa.UsuarioId;
            clienteEmpresa.ServicioSolicitadoId = request.ServicioSolicitadoId != 0 ? request.ServicioSolicitadoId : clienteEmpresa.ServicioSolicitadoId;
            clienteEmpresa.RazonSocial = request.RazonSocial ?? clienteEmpresa.RazonSocial;
            clienteEmpresa.Telefono = request.Telefono != 0 ? request.Telefono : clienteEmpresa.Telefono;
            clienteEmpresa.Correo = request.Correo ?? clienteEmpresa.Correo;
            clienteEmpresa.PaginaWeb = request.PaginaWeb ?? clienteEmpresa.PaginaWeb;
            clienteEmpresa.FechaInicio = request.FechaInicio != default ? request.FechaInicio : clienteEmpresa.FechaInicio;
            clienteEmpresa.DireccionFisica = request.DireccionFisica ?? clienteEmpresa.DireccionFisica;
            clienteEmpresa.Ciudad = request.Ciudad ?? clienteEmpresa.Ciudad;
            clienteEmpresa.Departamento = request.Departamento ?? clienteEmpresa.Departamento;
            clienteEmpresa.TipoOrganizacionId = request.TipoOrganizacionId != 0 ? request.TipoOrganizacionId : clienteEmpresa.TipoOrganizacionId;
            clienteEmpresa.TipoEmpresaId = request.TipoEmpresaId != 0 ? request.TipoEmpresaId : clienteEmpresa.TipoEmpresaId;
            clienteEmpresa.TamanoEmpresaId = request.TamanoEmpresaId != 0 ? request.TamanoEmpresaId : clienteEmpresa.TamanoEmpresaId;
            clienteEmpresa.TipoContabilidadId = request.TipoContabilidadId != 0 ? request.TipoContabilidadId : clienteEmpresa.TipoContabilidadId;
            clienteEmpresa.NivelFormalizacionId = request.NivelFormalizacionId != 0 ? request.NivelFormalizacionId : clienteEmpresa.NivelFormalizacionId;
            clienteEmpresa.ParticipaGremio = request.ParticipaGremio;
            clienteEmpresa.BeneficiadoCde = request.BeneficiadoCde;
            clienteEmpresa.TipoCasoEnProceso = request.TipoCasoEnProceso ?? clienteEmpresa.TipoCasoEnProceso;
            clienteEmpresa.EmpleadosTiempoCompleto = request.EmpleadosTiempoCompleto != 0 ? request.EmpleadosTiempoCompleto : clienteEmpresa.EmpleadosTiempoCompleto;
            clienteEmpresa.EmpleadosMedioTiempo = request.EmpleadosMedioTiempo != null ? request.EmpleadosMedioTiempo : clienteEmpresa.EmpleadosMedioTiempo;
            clienteEmpresa.TrabajadoresInformales = request.TrabajadoresInformales != null ? request.TrabajadoresInformales : clienteEmpresa.TrabajadoresInformales;
            clienteEmpresa.NegocioEnLinea = request.NegocioEnLinea;
            clienteEmpresa.NegocioEnCasa = request.NegocioEnCasa;
            clienteEmpresa.ComercioInternacionalId = request.ComercioInternacionalId != null ? request.ComercioInternacionalId : clienteEmpresa.ComercioInternacionalId;
            clienteEmpresa.PaisExporta = request.PaisExporta ?? clienteEmpresa.PaisExporta;
            clienteEmpresa.ContratoGobierno = request.ContratoGobierno;
            clienteEmpresa.ZonaIndigena = request.ZonaIndigena;
            clienteEmpresa.FuenteFinanciamientoId = request.FuenteFinanciamientoId != 0 ? request.FuenteFinanciamientoId : clienteEmpresa.FuenteFinanciamientoId;
            clienteEmpresa.SubFuenteFinanciamientoId = request.SubFuenteFinanciamientoId != null ? request.SubFuenteFinanciamientoId : clienteEmpresa.SubFuenteFinanciamientoId;
            clienteEmpresa.IngresosBrutosAnuales = request.IngresosBrutosAnuales != 0 ? request.IngresosBrutosAnuales : clienteEmpresa.IngresosBrutosAnuales;
            clienteEmpresa.FechaIngresosBrutos = request.FechaIngresosBrutos != default ? request.FechaIngresosBrutos : clienteEmpresa.FechaIngresosBrutos;
            clienteEmpresa.IngresosExportaciones = request.IngresosExportaciones != null ? request.IngresosExportaciones : clienteEmpresa.IngresosExportaciones;
            clienteEmpresa.GananciasPerdidasBrutas = request.GananciasPerdidasBrutas != null ? request.GananciasPerdidasBrutas : clienteEmpresa.GananciasPerdidasBrutas;
            clienteEmpresa.FechaGananciasPerdidasBrutas = request.FechaGananciasPerdidasBrutas != default ? request.FechaGananciasPerdidasBrutas : clienteEmpresa.FechaGananciasPerdidasBrutas;
            clienteEmpresa.DescripcionProductoServicio = request.DescripcionProductoServicio ?? clienteEmpresa.DescripcionProductoServicio;
            clienteEmpresa.AreasADominar = request.AreasADominar ?? clienteEmpresa.AreasADominar;
            clienteEmpresa.Instrucciones = request.Instrucciones ?? clienteEmpresa.Instrucciones;
            clienteEmpresa.Motivacion = request.Motivacion ?? clienteEmpresa.Motivacion;
            clienteEmpresa.LugarDesarrolloEmprendimiento = request.LugarDesarrolloEmprendimiento ?? clienteEmpresa.LugarDesarrolloEmprendimiento;
            clienteEmpresa.Obstaculos = request.Obstaculos ?? clienteEmpresa.Obstaculos;
            clienteEmpresa.FondoConcursable = request.FondoConcursable ?? clienteEmpresa.FondoConcursable;
            clienteEmpresa.EstatusInicial = request.EstatusInicial ?? clienteEmpresa.EstatusInicial;
            clienteEmpresa.EstatusActual = request.EstatusActual ?? clienteEmpresa.EstatusActual;
            clienteEmpresa.FechaEstablecimiento = request.FechaEstablecimiento != default ? request.FechaEstablecimiento : clienteEmpresa.FechaEstablecimiento;
            clienteEmpresa.NombrePropietarioId = request.NombrePropietarioId != 0 ? request.NombrePropietarioId : clienteEmpresa.NombrePropietarioId;
            clienteEmpresa.GeneroPropietario = request.GeneroPropietario ?? clienteEmpresa.GeneroPropietario;
            clienteEmpresa.HaSolicitadoCredito = request.HaSolicitadoCredito;
            clienteEmpresa.ComoSolicitoCredito = request.ComoSolicitoCredito ?? clienteEmpresa.ComoSolicitoCredito;
            clienteEmpresa.PorQueNoCredito = request.PorQueNoCredito ?? clienteEmpresa.PorQueNoCredito;
            clienteEmpresa.UsaPagoElectronico = request.UsaPagoElectronico;
            clienteEmpresa.MediosPago = request.MediosPago ?? clienteEmpresa.MediosPago;
            clienteEmpresa.Notas = request.Notas ?? clienteEmpresa.Notas;

            var resultado = await _context.SaveChangesAsync();
            if (resultado > 0)
            {
                return Unit.Value; // Retorna un valor vacío si la actualización fue exitosa
            }
            
            throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                new { mensaje = "No se pudo actualizar el cliente o empresa" });
        }
    }
}