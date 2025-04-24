using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos.Origem;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class OrigemController : Controller
    {
        private readonly IOrigemServico _origemServico;

        public OrigemController(IOrigemServico origemServico)
        {
            _origemServico = origemServico;
        }

        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            // Obtenha os dados paginados
            var resultado = await _origemServico.ObterTodosPaginados(page, pageSize);

            // Verifica se é uma requisição AJAX
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Retorna apenas o partial view com os dados atualizados
                return PartialView("_Listagem", resultado);
            }

            // Retorna a página completa
            return View("Consulta", resultado);
        }



        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(OrigemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _origemServico.Cadastrar(model);
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
            var usuario = await _origemServico.ObterPorId(id);
            if (usuario != null)
            {
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(OrigemViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var resultado = await _origemServico.Atualizar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction("Consulta");
        }
    }
}
