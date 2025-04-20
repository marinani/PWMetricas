using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Aplicacao.Modelos.Perfil;

namespace PWMetricas.Adm.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IPerfilServico _perfilServico;

        public PerfilController(IPerfilServico perfilServico)
        {
            _perfilServico = perfilServico;
        }

        // Tela de Consulta com Paginação
        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var perfis = await _perfilServico.ObterTodosPaginados(page, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_PerfilListagem", perfis);
            }

            return View(perfis);
        }

        // Tela de Cadastro
        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Novo(PerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resultado = await _perfilServico.CadastrarPerfil(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // Tela de Edição
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var perfil = await _perfilServico.ObterPorId(id);
            if (perfil == null)
                return NotFound();

            return View(perfil);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(PerfilViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var resultado = await _perfilServico.AtualizarPerfil(model);
        //    if (!resultado.Sucesso)
        //    {
        //        ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
        //        return View(model);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        //// Exclusão
        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var resultado = await _perfilServico.RemoverPerfil(id);
        //    if (!resultado.Sucesso)
        //    {
        //        TempData["Erro"] = string.Join(", ", resultado.Erros);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
