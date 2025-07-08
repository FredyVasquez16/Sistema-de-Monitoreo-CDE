using System.Net;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
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
        //public string Unidad { get; set; }

        public List<string> ListaAsesores { get; set; }
        public List<int> ListaContactos { get; set; }
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
        private readonly ICodigoUnicoGenerator _codigoUnicoGenerator;

        public Manejador(SistemaMonitoreaCdeContext context, ICodigoUnicoGenerator codigoUnicoGenerator)
        {
            _context = context;
            _codigoUnicoGenerator = codigoUnicoGenerator;
        }

        public async Task<Unit> Handle(AsesoriaCreateEjecuta request, CancellationToken cancellationToken)
        {
            /*
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

            if (request.ListaAsesores != null)
            {
                foreach (var id in request.ListaAsesores)
                {
                    //validae que el asesor existe
                    var asesor = await _context.Users.FindAsync(id);
                    if (asesor == null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                            new { mensaje = $"El asesor con el Id {id} no fue encontrado." });
                    }

                    /*var asesor = await _context.AsesoriasAsesores.FindAsync(id);
                    if (asesor == null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                            new { mensaje = $"El asesor con el Id {id} no fue encontrado." });
                    }*/
            /*var asesoriaId = await _context.Asesorias.FindAsync(asesoria.Id);
            if (asesoriaId == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                    new { mensaje = $"La asesoría con el Id {asesoria.Id} no fue encontrada." });
            }*/
            /*

            // Asignar el asesor a la asesoría
            asesoria.Asesores.Add(asesor);*/
/*
                    var asesoriaAsesor = new AsesoriasAsesores
                    {
                        AsesoriaId = asesoria.Id,
                        AsesorId = id
                    };
                    _context.AsesoriasAsesores.Add(asesoriaAsesor);
                }
            }

            if (valor <= 0)
            {
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError,
                    new { mensaje = "No se pudo crear la asesoría" });
            }

            // 🎯 Paso extra: generar y guardar el código único personalizado
            asesoria.CodigoUnico = _codigoUnicoGenerator.GenerarCodigo("AS", asesoria.Id);
            await _context.SaveChangesAsync();

            return Unit.Value;
            */


            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // Validaciones
                var cliente = await _context.ClientesEmpresas.FindAsync(request.ClienteId);
                if (cliente == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new { mensaje = $"El cliente con el Id {request.ClienteId} no fue encontrado." });

                var tipoContacto = await _context.TiposContactos.FindAsync(request.TipoContactoId);
                if (tipoContacto == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new { mensaje = $"El tipo de contacto con el Id {request.TipoContactoId} no fue encontrado." });

                var areaAsesoria = await _context.AreasAsesorias.FindAsync(request.AreaAsesoriaId);
                if (areaAsesoria == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new { mensaje = $"El área de asesoría con el Id {request.AreaAsesoriaId} no fue encontrada." });

                var fuenteFinanciamiento =
                    await _context.FuenteFinanciamientos.FindAsync(request.FuenteFinanciamientoId);
                if (fuenteFinanciamiento == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                        new
                        {
                            mensaje =
                                $"La fuente de financiamiento con el Id {request.FuenteFinanciamientoId} no fue encontrada."
                        });

                // Crear asesoría
                var asesoria = new Dominio.Asesoria
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
                await _context.SaveChangesAsync(cancellationToken); // Guarda para obtener el ID generado

                // Asignar asesores
                if (request.ListaAsesores != null)
                {
                    foreach (var id in request.ListaAsesores)
                    {
                        var asesor = await _context.Users.FindAsync(id);
                        if (asesor == null)
                            throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                                new { mensaje = $"El asesor con el Id {id} no fue encontrado." });

                        _context.AsesoriasAsesores.Add(new AsesoriaAsesor
                        {
                            AsesoriaId = asesoria.Id,
                            AsesorId = id
                        });
                    }
                }
                
                // Asignar contactos
                if (request.ListaContactos != null)
                {
                    foreach (var contactoId in request.ListaContactos)
                    {
                        var contacto = await _context.Contactos.FindAsync(contactoId);
                        if (contacto == null)
                            throw new ManejadorExcepcion(HttpStatusCode.NotFound,
                                new { mensaje = $"El contacto con el Id {contactoId} no fue encontrado." });

                        _context.AsesoriasContactos.Add(new AsesoriaContacto
                        {
                            ContactoId = contacto.Id,
                            AsesoriaId = asesoria.Id,
                            ClienteEmpresaId = request.ClienteId // Asignar cliente empresa
                        });
                    }
                }

                // Generar código único y guardar 
                asesoria.CodigoUnico = _codigoUnicoGenerator.GenerarCodigo("AS", asesoria.Id);
                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken); // Confirmar
                return Unit.Value;
            }
            catch (Exception)
            {
                //Console.WriteLine($"Error: {ex.Message}");
                await transaction.RollbackAsync(cancellationToken); // Revertirtodo en caso de error
                throw;
            }
        }
    }
}