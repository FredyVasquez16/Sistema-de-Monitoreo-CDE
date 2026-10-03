using Aplicacion.Asesorias;
using Aplicacion.ClientesEmpresas.DTOs;
using AutoMapper;
using Dominio;

namespace Aplicacion.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*CreateMap<Asesoria, AsesoriaDto>()
            .ForMember(x => x.Asesores, y => y.MapFrom(z => z.Asesores.Select(a => a.Asesor).ToList()));
        CreateMap<AsesoriaAsesor, AsesoriaAsesorDto>();
        CreateMap<Usuario, AsesorDto>();
        CreateMap<Contacto, ContactoDto>();*/

        CreateMap<Asesoria, AsesoriaDto>()
            .ForMember(x => x.Asesores, y => y.MapFrom(z => z.Asesores.Select(a => a.Asesor).ToList()))
            .ForMember(x => x.Contactos, y => y.MapFrom(z => z.AsesoriasContactos))
            // Nombres enriquecidos para las vistas
            .ForMember(x => x.ClienteNombre, y => y.MapFrom(z => z.Cliente != null ? (z.Cliente.RazonSocial ?? z.Cliente.Nombre) : null))
            .ForMember(x => x.TipoContactoNombre, y => y.MapFrom(z => z.TiposContacto != null ? z.TiposContacto.Descripcion : null))
            .ForMember(x => x.AreaAsesoriaNombre, y => y.MapFrom(z => z.AreaAsesoria != null ? z.AreaAsesoria.Descripcion : null))
            .ForMember(x => x.FuenteFinanciamientoNombre, y => y.MapFrom(z => z.FuenteFinanciamiento != null ? z.FuenteFinanciamiento.Descripcion : null));
        CreateMap<AsesoriaAsesor, AsesoriaAsesorDto>();
        CreateMap<Usuario, AsesorDto>();
        
        CreateMap<Contacto, ContactoDto>();
        
        CreateMap<AsesoriaContacto, AsesoriaContactoDto>()
            .ForMember(x => x.ContactoNombre, y => y.MapFrom(z => z.Contacto != null ? $"{z.Contacto.Nombre} {z.Contacto.Apellido}".Trim() : null))
            .ForMember(x => x.ClienteEmpresaNombre, y => y.MapFrom(z => z.ClienteEmpresa != null ? (z.ClienteEmpresa.RazonSocial ?? z.ClienteEmpresa.Nombre) : null));
        CreateMap<Contacto, ContactoDAsesoriaDto>();
        
        /*CreateMap<Dominio.ClientesEmpresas, ClienteEmpresaDto>()
            .ForMember(dest => dest.ContactoPrimarioNombre, opt => opt.MapFrom(src => src.ContactoPrimario != null ? $"{src.ContactoPrimario.Nombre} {src.ContactoPrimario.Apellido}" : ""))
            .ForMember(dest => dest.AsesorPrincipalNombre, opt => opt.MapFrom(src => src.Usuario != null ? $"{src.Usuario.NombreCompleto}" : ""))
            .ForMember(dest => dest.contactoPrimarioId, opt => opt.MapFrom(src => src.ContactoPrimarioId))
            .ForMember(dest => dest.usuarioId, opt => opt.MapFrom(src => src.UsuarioId));
        */
        CreateMap<Dominio.ClientesEmpresas, ClienteEmpresaDto>()
            .ForMember(dest => dest.ContactoPrimarioNombre, opt => opt.MapFrom(src => 
                src.ContactoPrimario != null ? $"{src.ContactoPrimario.Nombre} {src.ContactoPrimario.Apellido}" : string.Empty
            ))
            .ForMember(dest => dest.AsesorPrincipalNombre, opt => opt.MapFrom(src =>
                src.Usuario != null ? src.Usuario.NombreCompleto : string.Empty
            ))
            // CAMBIO: Añadimos el mapeo explícito para los IDs (buena práctica)
            .ForMember(dest => dest.contactoPrimarioId, opt => opt.MapFrom(src => src.ContactoPrimarioId))
            .ForMember(dest => dest.usuarioId, opt => opt.MapFrom(src => src.UsuarioId));
    }
}