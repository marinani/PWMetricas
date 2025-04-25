using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class AtendimentoController : Controller
    {
        private readonly IOrigemServico _origemServico;
        private readonly ITamanhoServico _tamanhoServico;
        private readonly ICanalServico _canalServico;
        private readonly IProdutoServico _produtoServico;
        private readonly IClienteServico _clienteServico;
        private readonly ILojaServico _lojaServico;
        private readonly IStatusAtendimentoServico _statusServico;
        //private readonly IAtendimentoServico _atendimentoServico;
        private readonly IUsuarioServico _usuarioServico;
        public AtendimentoController(IOrigemServico origemServico, ITamanhoServico tamanhoServico,
            ICanalServico canalServico, IProdutoServico produtoServico, IClienteServico clienteServico, ILojaServico lojaServico, IStatusAtendimentoServico statusServico, IUsuarioServico usuarioServico)
        {
            _origemServico = origemServico;
            _tamanhoServico = tamanhoServico;
            _canalServico = canalServico;
            _produtoServico = produtoServico;
            _clienteServico = clienteServico;
            _lojaServico = lojaServico;
            _statusServico = statusServico;
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        [Route("Atendimento/Consulta")]
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        [Route("Atendimento/NovoAtendimento")]
        public async Task<IActionResult> NovoAtendimento()
        {
            await CarregarCombos();
            //Obter o ID do usuário logado
            var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
            var usuarioLogadoId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (perfil != null && perfil.Equals("Vendedor"))
            {
                var modelo = new AtendimentoViewModel
                {
                    UsuarioId = usuarioLogadoId.Equals("0") ? 0 : int.Parse(usuarioLogadoId),
                    IsVendedor = true
                };

                return View(modelo);
            }
            else
            {
                return View();
            }



              


            
           
        }



        [HttpPost]
        [Route("Atendimento/NovoAtendimento")]
        public async Task<IActionResult> NovoAtendimento(AtendimentoViewModel atendimento)
        {
            await CarregarCombos();
            return View(atendimento);
        }


        #region Private Methods
        private async Task CarregarCombos()
        {
            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.NomeFantasia, CNPJ = c.CNPJ }).ToList();

            ViewBag.Canais = (await _canalServico.ObterTodos())
                        .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Origens = (await _origemServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Produtos = (await _produtoServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Tamanhos = (await _tamanhoServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Status = (await _statusServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();


            ViewBag.Clientes = (await _clienteServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome + " - " + c.Telefone }).ToList();


            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


            ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();
        }
        #endregion
    }
}
