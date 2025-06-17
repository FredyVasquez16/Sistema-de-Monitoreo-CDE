using Aplicacion;
using Aplicacion.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MyControllerBase
    {
        
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.LoginEjecuta parametros)
        {
            var data = await Mediator.Send(parametros);
            return StatusCode(StatusCodes.Status200OK, new ResponseDto<UsuarioData>
            {
                Status = true,
                Message = "Login exitoso",
                Data = data
            });
        }

        [HttpPost("signin")]
        public async Task<ActionResult<UsuarioData>> SignIn(SignIn.SignInEjecuta parametros)
        {
            var data = await Mediator.Send(parametros);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<UsuarioData>
            {
                Status = true,
                Message = "Usuario creado exitosamente",
                Data = data
            });
        }
        
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> UsuarioActual()
        {
            var data = await Mediator.Send(new UsuarioActual.UsuarioActualEjecuta());
            return StatusCode(StatusCodes.Status200OK, new ResponseDto<UsuarioData>
            {
                Status = true,
                Message = "Usuario actual obtenido exitosamente",
                Data = data
            });
        }
    }
}
