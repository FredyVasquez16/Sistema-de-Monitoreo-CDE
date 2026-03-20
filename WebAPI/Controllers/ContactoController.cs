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
        public async Task<ActionResult<ResponseDto<IReadOnlyList<Contacto>>>> Get()
        {
            var contactos = await Mediator.Send(new Consulta.ListaContactos());
            return Ok(new ResponseDto<IReadOnlyList<Contacto>>
            {
                Status = true,
                Data = contactos
            });
        }
        
        //endpoint para obtener los estados civiles
        [HttpGet("EstadosCiviles")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<EstadosCiviles>>>> GetEstadosCiviles()
        {
            var estadosCiviles = await Mediator.Send(new EstadoCivilGet.EstadoCivilGetEjecuta());
            return Ok(new ResponseDto<IReadOnlyList<EstadosCiviles>>
            {
                Status = true,
                Data = estadosCiviles
            });
        }
        
        //endpoint para obtener los niveles de estudio
        [HttpGet("NivelesEstudio")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<NivelesEstudio>>>> GetNivelesEstudio()
        {
            var nivelesEstudio = await Mediator.Send(new NivelEstudioGet.NivelEstudioGetEjecuta());
            return Ok(new ResponseDto<IReadOnlyList<NivelesEstudio>>
            {
                Status = true,
                Data = nivelesEstudio
            });
        }
        
        //endpoint para obtener las categorias laborales
        [HttpGet("CategoriasLaborales")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<CategoriasLaborales>>>> GetCategoriasLaborales()
        {
            var categoriasLaborales = await Mediator.Send(new CategoriaLaboralGet.CategoriaLaboralGetEjecuta());
            return Ok(new ResponseDto<IReadOnlyList<CategoriasLaborales>>
            {
                Status = true,
                Data = categoriasLaborales
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto<Contacto>>> GetById(int id)
        {
            var contacto = await Mediator.Send(new ContactoGetById.ContactoUnico { Id = id });
            if (contacto == null)
            {
                return NotFound(new ResponseDto<Contacto>
                {
                    Status = false,
                    Message = $"El contacto con el Id {id} no fue encontrado."
                });
            }
            return Ok(new ResponseDto<Contacto>
            {
                Status = true,
                Data = contacto
            });
        }
        
        // Endpoint para obtener las asesorías de un contacto
        [HttpGet("{id:int}/Asesorias")]
        public async Task<ActionResult<ResponseDto<List<Aplicacion.Contactos.ContactoGetAsesorias.ContactoAsesoriaDto>>>> GetAsesorias(int id)
        {
            var asesorias = await Mediator.Send(new ContactoGetAsesorias.Query { ContactoId = id });
            return Ok(new ResponseDto<List<Aplicacion.Contactos.ContactoGetAsesorias.ContactoAsesoriaDto>>
            {
                Status = true,
                Data = asesorias
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
