using AutoMapper;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configuração de mapeamento entre Perfil e PerfilViewModel
            CreateMap<Perfil, PerfilViewModel>().ReverseMap();
        }
    }
}
