using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PWMetricas.Api.Controllers
{
    /// <summary>
    /// Controller geral de Perfil
    /// </summary>
    /// <seealso cref="Controller" />
    [Produces("application/json")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly IPerfilServico _perfilServico;

        public PerfilController(IPerfilServico perfilServico)
        {
            _perfilServico = perfilServico;
        }


        [Authorize] // Deve exigir claim Operacoes.Listar = Permitido
        [HttpGet]
        [Route("api/Perfil/teste")]
        public IActionResult Get() => Ok("Acesso autorizado!");

        /// <summary>
        /// Lista todos os perfils.
        /// </summary>
        /// <returns></returns>
        [Route("Api/Perfil/ListarTodos")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ListarTodos()
        {
            var profiles = await _perfilServico.ObterTodos();
            return Ok(profiles);
        }

        /// <summary>
        /// Lista o perfil por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Api/Perfil/Listar/{id}")]
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Listar(int id)
        {

            var usuario = await _perfilServico.ObterPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
            
        }

        /// <summary>
        /// Adicionar um novo perfil.
        /// </summary>
        /// <returns></returns>
        [Route("Api/Perfil/Adicionar")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(PerfilViewModel model)
        {
            var resultado = await _perfilServico.Cadastrar(model);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Erros);
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Atualiza o perfil.
        /// </summary>
        /// <returns></returns>
        [Route("Api/Perfil/Atualizar")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Atualizar(PerfilViewModel model)
        {
            var resultado = await _perfilServico.Atualizar(model);
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Erros);
            }
            return Ok(resultado);
        }

      
    }
}
