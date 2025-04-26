using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Loja;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class LojaController : Controller
    {
        private readonly ILojaServico _lojaServico;
        public LojaController(ILojaServico lojaServico)   {
            _lojaServico = lojaServico;
        }

        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var resultado = await _lojaServico.ObterTodosPaginados(page, pageSize);

            
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
        public async Task<IActionResult> Cadastro(LojaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await _lojaServico.Cadastrar(modelo);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(modelo);
            }

            return RedirectToAction("Consulta");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(Guid guid)
        {
            var loja = await _lojaServico.ObterPorGuid(guid);
            if (loja != null)
            {
                
                return View(loja);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(LojaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await _lojaServico.Atualizar(modelo);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(modelo);
            }

            return RedirectToAction("Consulta");
        }
    }
}
