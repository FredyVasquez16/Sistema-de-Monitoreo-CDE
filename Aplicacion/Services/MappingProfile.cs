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
        
    }
}