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
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioConsulta>().ReverseMap();
            CreateMap<Perfil, PerfilSelect>().ReverseMap();
        }
    }
}
