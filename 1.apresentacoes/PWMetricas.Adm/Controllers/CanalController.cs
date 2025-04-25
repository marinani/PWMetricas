using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos;
using PWMetricas.Aplicacao.Modelos.Canal;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class CanalController : Controller
    {
        private readonly ICanalServico _canalServico;
        public CanalController(ICanalServico canalServico)
        {
            _canalServico = canalServico;
        }

        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var resultado = await _canalServico.ObterTodosPaginados(page, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Listagem", resultado);
            }

            return View("Consulta", resultado);
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CanalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _canalServico.Cadastrar(model);
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
            var usuario = await _canalServico.ObterPorId(id);
            if (usuario != null)
            {
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CanalViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var resultado = await _canalServico.Atualizar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction("Consulta");
        }
    }
}
