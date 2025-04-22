using AutoMapper;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configuração de mapeamento entre Perfil e PerfilViewModel
            CreateMap<Perfil, PerfilViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioConsulta>().ReverseMap();
            CreateMap<Perfil, PerfilSelect>().ReverseMap();

            // Configuração de mapeamento entre Usuario e UsuarioViewModel  
            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.PerfilNome, opt => opt.MapFrom(src => src.Perfil.Nome))
                .ReverseMap()
                .ForMember(dest => dest.Perfil, opt => opt.Ignore()); // Ignora a propriedade Perfil na reversão
        }
    }
}
