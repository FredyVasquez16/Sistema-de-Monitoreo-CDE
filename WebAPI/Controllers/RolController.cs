using Aplicacion;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : MyControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RolCreate.EjecutaCreate parametros)
        {
            var resultado = await Mediator.Send(parametros);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<Unit>
            {
                Status = true,
                Message = "Rol creado exitosamente",
                Data = resultado
            });
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(RolDelete.EjecutaDelete parametros)
        {
            var resultado = await Mediator.Send(parametros);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Rol eliminado exitosamente"
            });
        }
    }
}
