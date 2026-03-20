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
            .ForMember(x => x.Contactos, y => y.MapFrom(z => z.AsesoriasContactos));
        CreateMap<AsesoriaAsesor, AsesoriaAsesorDto>();
        CreateMap<Usuario, AsesorDto>();
        
        CreateMap<Contacto, ContactoDto>();
        
        CreateMap<AsesoriaContacto, AsesoriaContactoDto>();
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