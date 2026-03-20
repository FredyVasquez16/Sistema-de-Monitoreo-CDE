using Aplicacion;
using Aplicacion.ClientesEmpresas;
using Aplicacion.ClientesEmpresas.DTOs;
using Aplicacion.Contactos;
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
// El endpoint ahora devuelve una lista de ClienteEmpresaDto
        public async Task<ActionResult<ResponseDto<List<ClienteEmpresaDto>>>> Get()
        {
            var clientes = await Mediator.Send(new ClienteEmpresaGet.ListaClientesEmpresas());
            return new ResponseDto<List<ClienteEmpresaDto>>
            {
                Status = true,
                Data = clientes
            };
        }

        //endpoint para obtener los TiposClienteNivel
        [HttpGet("TiposClientesNivel")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposClienteNivel>>>> GetTiposClienteNivel()
        {
            var tiposClienteNivel = await Mediator.Send(new TipoClienteNivelGet.ListaTiposClienteNivel());
            return Ok(new ResponseDto<IReadOnlyList<TiposClienteNivel>>
            {
                Status = true,
                Data = tiposClienteNivel
            });
        }

        //endpoint para obtener los ServiciosSolicitados
        [HttpGet("ServiciosSolicitados")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<ServiciosSolicitados>>>> GetServiciosSolicitados()
        {
            var serviciosSolicitados = await Mediator.Send(new ServiciosSolicitadosGet.ListaServiciosSolicitados());
            return Ok(new ResponseDto<IReadOnlyList<ServiciosSolicitados>>
            {
                Status = true,
                Data = serviciosSolicitados
            });
        }

        //endpoint para obtener los TiposClienteEstado
        [HttpGet("TiposClientesEstado")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposClientesEstado>>>> GetTiposClienteEstado()
        {
            var tiposClienteEstado = await Mediator.Send(new TiposClienteEstadoGet.ListaTiposClienteEstado());
            return Ok(new ResponseDto<IReadOnlyList<TiposClientesEstado>>
            {
                Status = true,
                Data = tiposClienteEstado
            });
        }

        // Endpoint para obtener los TiposOrganizacion
        [HttpGet("TiposOrganizacion")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposOrganizacion>>>> GetTiposOrganizacion()
        {
            var tiposOrganizacion = await Mediator.Send(new TiposOrganizacionGet.ListaTiposOrganizacion());
            return Ok(new ResponseDto<IReadOnlyList<TiposOrganizacion>>
            {
                Status = true,
                Data = tiposOrganizacion
            });
        }

        //Endpoint para buscar obtener los TiposEmpresa
        [HttpGet("TiposEmpresa")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposEmpresa>>>> GetTiposEmpresa()
        {
            var tiposEmpresa = await Mediator.Send(new TiposEmpresaGet.ListaTiposEmpresa());
            return Ok(new ResponseDto<IReadOnlyList<TiposEmpresa>>
            {
                Status = true,
                Data = tiposEmpresa
            });
        }
        
        // Endpoint para obtener TamañoEmpresa
        [HttpGet("TamanoEmpresa")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TamanoEmpresas>>>> GetTamanoEmpresa()
        {
            var tamanoEmpresa = await Mediator.Send(new TamanoEmpresaGet.ListaTamanoEmpresa());
            return Ok(new ResponseDto<IReadOnlyList<TamanoEmpresas>>
            {
                Status = true,
                Data = tamanoEmpresa
            });
        }
        
        // Endpoint para buscar Tipos de Contabilidad
        [HttpGet("TiposContabilidad")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposContabilidad>>>> GetTiposContabilidad()
        {
            var tiposContabilidad = await Mediator.Send(new TipoContabilidadGet.ListaTiposContabilidad());
    
            return Ok(new ResponseDto<IReadOnlyList<TiposContabilidad>>
            {
                Status = true,
                Data = tiposContabilidad
            });
        }
        
        //Endpoint para obtener Niveles de Formalizacion
        [HttpGet("NivelesFormalizacion")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<NivelesFormalizacion>>>> GetNivelesFormalizacion()
        {
            var nivelesFormalizacion = await Mediator.Send(new NivelFormalizacionGet.ListaNivelFormalizacion());
            return Ok(new ResponseDto<IReadOnlyList<NivelesFormalizacion>>
            {
                Status = true,
                Data = nivelesFormalizacion
            });
        }
        
        // Endpoint para obtener los Tipos de Comercio Internacional
        [HttpGet("TiposComercioInternacional")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<TiposComerciosInternacional>>>> GetTiposComercioInternacional()
        {
            var tiposComercioInternacional = await Mediator.Send(new TipoComercioInternacionalGet.ListaTipoCoomercioInternacional());
            return Ok(new ResponseDto<IReadOnlyList<TiposComerciosInternacional>>
            {
                Status = true,
                Data = tiposComercioInternacional
            });
        }
        
        // Endpoint para obtener las Fuentes de Financiamiento
        [HttpGet("FuentesFinanciamiento")]
        public async Task<ActionResult<ResponseDto<IReadOnlyList<FuenteFinanciamiento>>>> GetFuentesFinanciamiento()
        {
            var fuentesFinanciamiento = await Mediator.Send(new FuenteFinanciamientoGet.ListaFuenteFinanciamiento());
            return Ok(new ResponseDto<IReadOnlyList<FuenteFinanciamiento>>
            {
                Status = true,
                Data = fuentesFinanciamiento
            });
        }

        [HttpGet("buscar")] // La ruta será /api/Contacto/buscar?termino=juan
        public async Task<ActionResult<ResponseDto<List<ContactoDto>>>> BuscarContactos([FromQuery] string termino)
        {
            // Usamos el mismo mediador que probablemente ya tienes para obtener la lista de contactos,
            // pero con un filtro. Si no tienes un DTO, puedes devolver List<Dominio.Contacto>.
            var contactos = await Mediator.Send(new Consulta.FiltroContactos() { TerminoBusqueda = termino });
    
            return new ResponseDto<List<ContactoDto>>
            {
                Status = true,
                Data = contactos
            };
        }
        
        [HttpGet("BuscarAsesores")]
        public async Task<ActionResult<ResponseDto<List<AsesorFiltroDto>>>> BuscarAsesores([FromQuery] string termino)
        {
            var asesores = await Mediator.Send(new AsesorFiltro.AsesorFiltroEjecuta { TerminoBusqueda = termino });
            
            return new ResponseDto<List<AsesorFiltroDto>>
            {
                Status = true,
                Data = asesores
            };
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
