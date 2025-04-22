using System;
using System.Text;
using AutoMapper;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dados.Repositorios.Interfaces;
using PWMetricas.Dominio.Entidades;
using System.Security.Cryptography;

namespace PWMetricas.Aplicacao.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;
        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IMapper mapper) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<UsuarioViewModel> ObterPorId(int id)
        {
            var usuario = await _usuarioRepositorio.BuscarPorId(id);
            return _mapper.Map<UsuarioViewModel>(usuario);
        }
        public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
        {
            var usuarios = await _usuarioRepositorio.ObterTodosAsync();
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);
        }
        public async Task<List<UsuarioViewModel>> ListarAtivos()
        {
            var usuarios = await _usuarioRepositorio.ObterTodosAtivos();
            return _mapper.Map<List<UsuarioViewModel>>(usuarios);
        }

        public async Task<Resultado> CadastrarUsuario(UsuarioViewModel modelo)
        {
            var resultado = new Resultado();

            if (!modelo.PerfilId.HasValue)
            {
                return new Resultado(new[] { "O campo perfil é obrigatório." });
            }

            try
            {
                // Fix: Initialize all required properties of the 'Perfil' class
                var usuario = new Usuario()
                {
                    Id = 0, // Assuming 0 for new entities; adjust as needed
                    Guid = Guid.NewGuid(), // Generate a new GUID
                    Nome = modelo.Nome,
                    Email = modelo.Email,
                    Senha = EncryptPassword(modelo.Senha), // Criptografa a senha
                    DataCadastro = DateTime.Now,
                    PerfilId = modelo.PerfilId.Value, // Use 0 or a default value if PerfilId is null
                    Ativo = true
                };

                await _usuarioRepositorio.Atualizar(usuario);

                var usuarioSalvo = await _usuarioRepositorio.BuscarPorId(usuario.Id);
                Console.WriteLine(usuarioSalvo != null ? "Usuário salvo com sucesso!" : "Erro ao salvar usuário.");

                return new Resultado("Sucesso ao cadastrar usuário.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao cadastrar usuário: " + ex.Message });
            }
        }

        public async Task<Resultado> EditarUsuario(UsuarioViewModel modelo)
        {
            var resultado = new Resultado();

            if (!modelo.PerfilId.HasValue)
            {
                return new Resultado(new[] { "O campo perfil é obrigatório." });
            }

            var usuario = await _usuarioRepositorio.BuscarPorId(modelo.Id);

            if (usuario == null)
            {
                return new Resultado(new[] { "Usuário não encontrado." });
            }   

            try
            {

                usuario.Nome = modelo.Nome;
                usuario.PerfilId = modelo.PerfilId.Value;
                //usuario.Ativo = modelo.Ativo;

                await _usuarioRepositorio.Atualizar(usuario);

               
                return new Resultado("Sucesso ao atualizar usuário.", usuario);
            }
            catch (Exception ex)
            {
                return new Resultado(new[] { "Erro ao atualizar usuário: " + ex.Message });
            }
        }

        public async Task<PaginacaoResultado<UsuarioConsulta>> ObterTodosPaginados(int page, int pageSize)
        {
            var perfis = await _usuarioRepositorio.ObterTodosPaginadosAsync(page, pageSize);
            var totalRegistros = await _usuarioRepositorio.ContarTotalAsync();

            return new PaginacaoResultado<UsuarioConsulta>
            {
                Dados = _mapper.Map<IEnumerable<UsuarioConsulta>>(perfis),
                PaginaAtual = page,
                TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)pageSize),
                TotalRegistros = totalRegistros
            };
        }


        public async Task<UsuarioViewModel?> AutenticarUsuario(string email, string senha)
        {
            var usuario = await _usuarioRepositorio.ObterPorEmailAsync(email);
            if (usuario == null)
            {
                return null;
            }

            // Verifica a senha
            var senhaValida = VerifyPassword(senha, usuario.Senha);
            if (!senhaValida)
            {
                return null;
            }

            return _mapper.Map<UsuarioViewModel>(usuario);
        }


        #region Métodos Privados
        // Método para criptografar a senha
        private string EncryptPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Método para descriptografar (não aplicável para hash, mas incluído para referência)
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            var inputPasswordHash = EncryptPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }
        #endregion

    }
}
