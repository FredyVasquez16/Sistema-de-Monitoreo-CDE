using Aplicacion;
using Aplicacion.Asesorias;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsesoriaController : MyControllerBase
    {
        //private readonly IMediator _mediator;
        private readonly SistemaMonitoreaCdeContext _context;
        public AsesoriaController(IMediator mediator, SistemaMonitoreaCdeContext context)
        {
            //_mediator = mediator;
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<Asesorias>>>> Get()
        {
            var asesorias = await Mediator.Send(new AsesoriaGet.ListaAsesorias());
            return Ok(new ResponseDto<IReadOnlyList<Asesorias>>
            {
                Status = true,
                Data = asesorias
            });
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<Asesorias>>> GetById(int id)
        {
            var asesoria = await Mediator.Send(new AsesoriaGetById.AsesoriaUnica { Id = id });
            if (asesoria == null)
            {
                return NotFound(new ResponseDto<Asesorias>
                {
                    Status = false,
                    Message = $"La asesoría con el Id {id} no fue encontrada."
                });
            }
            return Ok(new ResponseDto<Asesorias>
            {
                Status = true,
                Data = asesoria
            });
        }
        
        [HttpGet("archivo/{archivoId}")]
        public async Task<IActionResult> VerArchivo(int archivoId)
        {
            var archivo = await _context.AsesoriasArchivos.FindAsync(archivoId);

            if (archivo == null)
                return NotFound(new { mensaje = "Archivo no encontrado." });

            //return File(archivo.Contenido, archivo.TipoMime, archivo.NombreOriginal); // si quieres descargar con nombre original
            return File(archivo.Contenido, archivo.TipoMime, enableRangeProcessing: true); // si quieres que el navegador maneje el archivo (ej. imagen, pdf, etc.)
        }
        
        [HttpGet("{id}/archivos")]
        public async Task<IActionResult> VerArchivos(int id)
        {
            var archivos = await Mediator.Send(new AsesoriaGetArchivo.Query { AsesoriaId = id });

            return Ok(new ResponseDto<List<AsesoriaGetArchivo.ArchivoDto>>
            {
                Status = true,
                Data = archivos
            });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(AsesoriaCreate.AsesoriaCreateEjecuta data)
        {
            var dataAsesoria = await Mediator.Send(data);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<Unit>
            {
                Status = true,
                Message = "Asesoría creada exitosamente",
                Data = dataAsesoria
            });
        }
        
        /*[HttpPost("{id}/archivos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirArchivos(int id, [FromForm] SubidaArchivoDto dto)
        {
            try
            {
                var asesoria = await _context.Asesorias.FindAsync(id);
                if (asesoria == null)
                    return NotFound("Asesoría no encontrada.");

                foreach (var archivo in dto.Archivos)
                {
                    using var ms = new MemoryStream();
                    await archivo.CopyToAsync(ms);

                    var nuevoArchivo = new AsesoriasArchivos
                    {
                        AsesoriaId = id,
                        NombreOriginal = archivo.FileName,
                        Contenido = ms.ToArray(),
                        TipoMime = archivo.ContentType,
                        FechaSubida = DateTime.UtcNow
                    };

                    _context.AsesoriasArchivos.Add(nuevoArchivo);
                }

                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Archivos subidos correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }*/
        
        [HttpPost("{id}/archivos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SubirArchivos(int id, [FromForm] List<IFormFile> archivos)
        {
            var comando = new AsesoriaSubirArchivo.Comando
            {
                AsesoriaId = id,
                Archivos = archivos
            };

            await Mediator.Send(comando);

            return Ok(new ResponseDto<string>
            {
                Status = true,
                Message = "Archivos subidos correctamente"
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Unit>> Update(int id, AsesoriaUpdate.AsesoriaUpdateEjecuta data)
        {
            data.Id = id;
            var result = await Mediator.Send(data);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Asesoría actualizada exitosamente",
                Data = result
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            var result = await Mediator.Send(new AsesoriaDelete.AsesoriaDeleteEjecuta { Id = id });
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Asesoría eliminada exitosamente"
            });
        }
    }
}
