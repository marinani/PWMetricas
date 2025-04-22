using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PWMetricas.Aplicacao.Modelos.Tamanho;

namespace PWMetricas.Adm.Controllers
{

    [Authorize]
    public class TamanhoController : Controller
    {

        private readonly ITamanhoServico _tamanhoServico;

        public TamanhoController(ITamanhoServico tamanhoServico)
        {
            _tamanhoServico = tamanhoServico;
        }

        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var perfis = await _tamanhoServico.ObterTodosPaginados(page, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Listagem", perfis);
            }

            return View(perfis);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(TamanhoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _tamanhoServico.Cadastrar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _tamanhoServico.ObterPorId(id);
            if (usuario != null)
            {
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TamanhoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            var resultado = await _tamanhoServico.Atualizar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction("Consulta");
        }
    }
}
