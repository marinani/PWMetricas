using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Atendimento;
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
        public AtendimentoController(IOrigemServico origemServico, ITamanhoServico tamanhoServico,
            ICanalServico canalServico, IProdutoServico produtoServico, IClienteServico clienteServico, ILojaServico lojaServico) 
        {
            _origemServico = origemServico;
            _tamanhoServico = tamanhoServico;
            _canalServico = canalServico;
            _produtoServico = produtoServico;
            _clienteServico = clienteServico;
            _lojaServico = lojaServico;
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
            return View();
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


            ViewBag.Clientes = (await _clienteServico.ObterTodos())
           .Select(c => new SelectListItem
            {
            Value = c.Id.ToString(),
            Text = c.Nome + " - " + c.Telefone
           }).ToList();



            //ViewBag.Status = await _tamanhoServico.ObterTodos();
        }
        #endregion
    }
}
