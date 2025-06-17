using Aplicacion;
using Aplicacion.Capacitaciones;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapacitacionController : MyControllerBase
    {
        //private readonly IMediator _mediator;
        private readonly SistemaMonitoreaCdeContext _context;

        public CapacitacionController(IMediator mediator, SistemaMonitoreaCdeContext context)
        {
            //_mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<Capacitaciones>>>> Get()
        {
            var capacitaciones = await Mediator.Send(new CapacitacionGet.ListaCapacitaciones());
            return Ok(new ResponseDto<IReadOnlyList<Capacitaciones>>
            {
                Status = true,
                Data = capacitaciones
            });
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<Capacitaciones>>> GetById(int id)
        {
            var capacitacion = await Mediator.Send(new CapacitacionGetById.CapacitacionUnica { Id = id });
            if (capacitacion == null)
            {
                return NotFound(new ResponseDto<Capacitaciones>
                {
                    Status = false,
                    Message = $"La capacitación con el Id {id} no fue encontrada."
                });
            }
            return Ok(new ResponseDto<Capacitaciones>
            {
                Status = true,
                Data = capacitacion
            });
        }

        [HttpGet("archivo/{archivoId}")]
        public async Task<IActionResult> VerArchivo(int archivoId)
        {
            var archivo = await _context.CapacitacionesArchivos.FindAsync(archivoId);
            if (archivo == null)
            {
                return NotFound(new { mensaje = "Archivo no encontrado." });
            }
            return File(archivo.Contenido, archivo.TipoMime, archivo.NombreOriginal); // si quieres descargar con nombre original
            
        }
        
        [HttpGet("{id}/archivo")]
        public async Task<IActionResult> VerArchivos(int id)
        {
            var archivos = await Mediator.Send(new CapacitacionGetArchivo.Query { CapacitacionId = id });
            if (archivos == null || !archivos.Any())
            {
                return NotFound(new ResponseDto<List<CapacitacionGetArchivo.ArchivoDto>>
                {
                    Status = false,
                    Message = $"No se encontraron archivos para la capacitación con el Id {id}."
                });
            }
            return Ok(new ResponseDto<List<CapacitacionGetArchivo.ArchivoDto>>
            {
                Status = true,
                Data = archivos
            });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(CapacitacionCreate.CapacitacionCreateEjecuta data)
        {
            var dataCapacitacion = await Mediator.Send(data);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<Unit>
            {
                Status = true,
                Message = "Capacitación creada exitosamente",
                Data = dataCapacitacion
            });
        }

        [HttpPost("{id}/archivo")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> SubirArchivo(int id, [FromForm] List<IFormFile> archivos)
        {
            var comando = new CapacitacionSubirArchivo.Comando
            {
                CapacitacionId = id,
                Archivos = archivos
            };
            await Mediator.Send(comando);
            
            return Ok(new ResponseDto<string>
            {
                Status = true,
                Message = "Archivos subidos exitosamente"
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Unit>> Update(int id, CapacitacionUpdate.CapacitacionUpdateEjecuta data)
        {
            data.Id = id;
            var result = await Mediator.Send(data);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Capacitación actualizada exitosamente",
                Data = result
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            var result = await Mediator.Send(new CapacitacionDelete.CapacitacionDeleteEjecuta { Id = id });
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Capacitación eliminada exitosamente",
            });
        }
    }
}
