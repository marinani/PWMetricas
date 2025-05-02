using AutoMapper;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos.Canal;
using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Aplicacao.Modelos.Loja;
using PWMetricas.Aplicacao.Modelos.Origem;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos.Produto;
using PWMetricas.Aplicacao.Modelos.StatusAtendimento;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Aplicacao
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configuração de mapeamento entre Entidades e ViewModel
            CreateMap<Perfil, PerfilViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteSimplesViewModel>().ReverseMap();
            CreateMap<Loja, LojaViewModel>().ReverseMap();
            CreateMap<Loja, LojaSimplesViewModel>().ReverseMap();
            CreateMap<Canal, CanalViewModel>().ReverseMap();
            CreateMap<Tamanho, TamanhoViewModel>().ReverseMap();
            CreateMap<Origem, OrigemViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Perfil, PerfilViewModel>().ReverseMap();
            CreateMap<Perfil, PerfilSelect>().ReverseMap();
            CreateMap<Usuario, UsuarioConsulta>()
                .ForMember(dest => dest.LojaNome, opt => opt.MapFrom(src => src.Loja.NomeFantasia))
                .ReverseMap()
                .ForMember(dest => dest.Loja, opt => opt.Ignore());
            CreateMap<Usuario, UsuarioSelect>().ReverseMap();
            CreateMap<Usuario, VendedorViewModel>().ReverseMap();
            CreateMap<StatusAtendimento, StatusAtendimentoViewModel>().ReverseMap();
            CreateMap<Atendimento, AtendimentoViewModel>().ReverseMap();
            CreateMap<AtendimentoObservacoes, ObservacoesAtendimentoViewModel>().ReverseMap();

            // Configuração de mapeamento entre Usuario e UsuarioViewModel  
            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.PerfilNome, opt => opt.MapFrom(src => src.Perfil.Nome))
                .ReverseMap()
                .ForMember(dest => dest.Perfil, opt => opt.Ignore()); // Ignora a propriedade Perfil na reversão
        }
    }
}
