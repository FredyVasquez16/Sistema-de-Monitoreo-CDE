using Aplicacion;
using Aplicacion.ClientesEmpresas;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteEmpresaController : MyControllerBase
    {
        /*private readonly IMediator _mediator;
        
        public ClienteEmpresaController(IMediator mediator)
        {
            _mediator = mediator;
        }*/
        
        [HttpGet]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<ClientesEmpresas>>>> Get()
        {
            var clientes = await Mediator.Send(new ClienteEmpresaGet.ListaClientesEmpresas());
            return Ok(new ResponseDto<IReadOnlyList<ClientesEmpresas>>
            {
                Status = true,
                Data = clientes
            });
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<ClientesEmpresas>>> GetById(int id)
        {
            var clienteEmpresa = await Mediator.Send(new ClienteEmpresaGetById.ClienteEmpresaUnico { Id = id });
            if (clienteEmpresa == null)
            {
                return NotFound(new ResponseDto<ClientesEmpresas>
                {
                    Status = false,
                    Message = $"El cliente o empresa con el Id {id} no fue encontrado."
                });
            }
            return Ok(new ResponseDto<ClientesEmpresas>
            {
                Status = true,
                Data = clienteEmpresa
            });
        }
        
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(ClienteEmpresaCreate.ClienteEmpresaCreateEjecuta data)
        {
            var dataCliente = await Mediator.Send(data);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<Unit>
            {
                Status = true,
                Message = "Cliente creado exitosamente",
                Data = dataCliente
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Unit>> Update(int id, ClienteEmpresaUpdate.ClienteEmpresaUpdateEjecuta data)
        {
            data.Id = id;
            var result = await Mediator.Send(data);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Cliente actualizado exitosamente",
                Data = result
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            var result = await Mediator.Send(new ClienteEmpresaDelete.ClienteEmpresaDeleteEjecuta { Id = id });
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Cliente eliminado exitosamente"
            });
        }
    }
}
