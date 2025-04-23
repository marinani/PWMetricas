using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    public class AtendimentoController : Controller
    {
        private readonly IOrigemServico _origemServico;
        private readonly ITamanhoServico _tamanhoServico;
        private readonly ICanalServico _canalServico;
        private readonly IProdutoServico _produtoServico;
        public AtendimentoController(IOrigemServico origemServico, ITamanhoServico tamanhoServico,
            ICanalServico canalServico, IProdutoServico produtoServico) 
        {
            _origemServico = origemServico;
            _tamanhoServico = tamanhoServico;
            _canalServico = canalServico;
            _produtoServico = produtoServico;
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
            // Preencher dados necessários para a view
            ViewBag.Canais = await _canalServico.ObterTodos();
            ViewBag.Origens = await _origemServico.ObterTodos();
            ViewBag.Produtos = await _produtoServico.ObterTodos();
            ViewBag.Tamanhos = await _tamanhoServico.ObterTodos();
        }
        #endregion
    }
}
