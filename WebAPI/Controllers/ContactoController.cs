using Aplicacion;
using Aplicacion.Contactos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : MyControllerBase
    {
        /*private readonly IMediator _mediator;

        public ContactoController(IMediator mediator)
        {
            _mediator = mediator;
        }*/
        
        [HttpGet]
        //[Authorize] //solo agregar si se requiere autenticación ya esta todo configurado en el proyecto
        public async Task<ActionResult<ResponseDto<IReadOnlyList<Contactos>>>> Get()
        {
            var contactos = await Mediator.Send(new Consulta.ListaContactos());
            return Ok(new ResponseDto<IReadOnlyList<Contactos>>
            {
                Status = true,
                Data = contactos
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<Contactos>>> GetById(int id)
        {
            var contacto = await Mediator.Send(new ContactoGetById.ContactoUnico { Id = id });
            if (contacto == null)
            {
                return NotFound(new ResponseDto<Contactos>
                {
                    Status = false,
                    Message = $"El contacto con el Id {id} no fue encontrado."
                });
            }
            return Ok(new ResponseDto<Contactos>
            {
                Status = true,
                Data = contacto
            });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(ContactoCreate.ContactoCreateEjecuta data)
        {
            var dataContacto = await Mediator.Send(data);
            return StatusCode(StatusCodes.Status201Created, new ResponseDto<Unit>
            {
                Status = true,
                Message = "Contacto creado exitosamente",
                Data = dataContacto
            });
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Unit>> Update(int id, ContactoUpdate.ContactoUpdateEjecuta data)
        {
            data.Id = id;
            var result = await Mediator.Send(data);
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Contacto actualizado exitosamente",
                Data = result
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            var result = await Mediator.Send(new ContactoDelete.ContactoDeleteEjecuta { Id = id });
            return Ok(new ResponseDto<Unit>
            {
                Status = true,
                Message = "Contacto eliminado exitosamente"
            });
        }
    }
}
