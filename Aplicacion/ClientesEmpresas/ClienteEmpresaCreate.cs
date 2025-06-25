using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.ClientesEmpresas;

public class ClienteEmpresaCreate
{
    public class ClienteEmpresaCreateEjecuta : IRequest
    {
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

    public class EjecutaValidacion : AbstractValidator<ClienteEmpresaCreateEjecuta>
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

    // Manejador para procesar la solicitud de creación de cliente empresa
    public class Manejador : IRequestHandler<ClienteEmpresaCreateEjecuta>
    {
        private readonly SistemaMonitoreaCdeContext _context;
        private readonly ICodigoUnicoGenerator _codigoUnicoGenerator;

        public Manejador(SistemaMonitoreaCdeContext context, ICodigoUnicoGenerator codigoUnicoGenerator)
        {
            _context = context;
            _codigoUnicoGenerator = codigoUnicoGenerator;
        }

        public async Task<Unit> Handle(ClienteEmpresaCreateEjecuta request, CancellationToken cancellationToken)
        {
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

            var clienteEmpresa = new Dominio.ClientesEmpresas
            {
                Nombre = request.Nombre,
                TipoClienteNivelId = request.TipoClienteNivelId,
                ContactoPrimarioId = request.ContactoPrimarioId,
                TipoClienteEstadoId = request.TipoClienteEstadoId,
                UsuarioId = request.UsuarioId,
                ServicioSolicitadoId = request.ServicioSolicitadoId,
                RazonSocial = request.RazonSocial,
                Telefono = request.Telefono,
                Correo = request.Correo,
                PaginaWeb = request.PaginaWeb,
                FechaInicio = request.FechaInicio,
                DireccionFisica = request.DireccionFisica,
                Ciudad = request.Ciudad,
                Departamento = request.Departamento,
                TipoOrganizacionId = request.TipoOrganizacionId,
                TipoEmpresaId = request.TipoEmpresaId,
                TamanoEmpresaId = request.TamanoEmpresaId,
                TipoContabilidadId = request.TipoContabilidadId,
                NivelFormalizacionId = request.NivelFormalizacionId,
                ParticipaGremio = request.ParticipaGremio,
                BeneficiadoCde = request.BeneficiadoCde,
                TipoCasoEnProceso = request.TipoCasoEnProceso,
                EmpleadosTiempoCompleto = request.EmpleadosTiempoCompleto,
                EmpleadosMedioTiempo = request.EmpleadosMedioTiempo,
                TrabajadoresInformales = request.TrabajadoresInformales,
                NegocioEnLinea = request.NegocioEnLinea,
                NegocioEnCasa = request.NegocioEnCasa,
                ComercioInternacionalId = request.ComercioInternacionalId,
                PaisExporta = request.PaisExporta,
                ContratoGobierno = request.ContratoGobierno,
                ZonaIndigena = request.ZonaIndigena,
                FuenteFinanciamientoId = request.FuenteFinanciamientoId,
                SubFuenteFinanciamientoId = request.SubFuenteFinanciamientoId,
                IngresosBrutosAnuales = request.IngresosBrutosAnuales,
                FechaIngresosBrutos = request.FechaIngresosBrutos,
                IngresosExportaciones = request.IngresosExportaciones,
                GananciasPerdidasBrutas = request.GananciasPerdidasBrutas,
                FechaGananciasPerdidasBrutas = request.FechaGananciasPerdidasBrutas,
                DescripcionProductoServicio = request.DescripcionProductoServicio,
                AreasADominar = request.AreasADominar,
                Instrucciones = request.Instrucciones,
                Motivacion = request.Motivacion,
                LugarDesarrolloEmprendimiento = request.LugarDesarrolloEmprendimiento,
                Obstaculos = request.Obstaculos,
                FondoConcursable = request.FondoConcursable,
                EstatusInicial = request.EstatusInicial,
                EstatusActual = request.EstatusActual,
                FechaEstablecimiento = request.FechaEstablecimiento,
                NombrePropietarioId = request.NombrePropietarioId,
                GeneroPropietario = request.GeneroPropietario,
                HaSolicitadoCredito = request.HaSolicitadoCredito,
                ComoSolicitoCredito = request.ComoSolicitoCredito,
                PorQueNoCredito = request.PorQueNoCredito,
                UsaPagoElectronico = request.UsaPagoElectronico,
                MediosPago = request.MediosPago,
                Notas = request.Notas
            };

            _context.ClientesEmpresas.Add(clienteEmpresa);
            var valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                clienteEmpresa.CodigoUnico = _codigoUnicoGenerator.GenerarCodigo("CE", clienteEmpresa.Id);
                await _context.SaveChangesAsync();
                
                return Unit.Value;
            }

            throw new ManejadorExcepcion(HttpStatusCode.BadRequest,
                new { mensaje = "No se pudo crear el cliente empresa" });
        }
    }
}