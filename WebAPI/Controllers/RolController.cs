using Aplicacion;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : MyControllerBase
    {
        [HttpGet("listar")]
        public async Task<ActionResult<List<IdentityRole>>> Listar()
        {
            var resultado = await Mediator.Send(new RolLista.EjecutaLista());
            return Ok(new ResponseDto<List<IdentityRole>>
            {
                Status = true,
                Data = resultado
            });
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuario(string username)
        {
            var resultado = await Mediator.Send(new ObtenerRolesPorUsuaio.EjecutaObtenerRolesPorUsuario { Username = username });
            return Ok(new ResponseDto<List<string>>
            {
                Status = true,
                Data = resultado
            });
        }
        
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
        
        [HttpPost("asignarRolAUsuario")]
        public async Task<ActionResult<Unit>> AsignarRolAUsuario(UsuarioRolAdd.EjecutaAdd parametros)
        {
            var resultado = await Mediator.Send(parametros);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Rol asignado al usuario exitosamente",
                Data = resultado
            });
        }
        
        [HttpPost("eliminarRolDeUsuario")]
        public async Task<ActionResult<Unit>> EliminarRolDeUsuario(UsuarioRolDelete.EjecutaUsuarioRolDelete parametros)
        {
            var resultado = await Mediator.Send(parametros);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Rol eliminado del usuario exitosamente",
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
