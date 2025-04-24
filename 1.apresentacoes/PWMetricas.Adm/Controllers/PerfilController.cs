using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos.Tamanho;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
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
                return PartialView("_Listagem", perfis);
            }

            return View(perfis);
        }

       
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(PerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resultado = await _perfilServico.Cadastrar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction(nameof(Consulta));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _perfilServico.ObterPorId(id);
            if (usuario != null)
            {
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(PerfilViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var resultado = await _perfilServico.Atualizar(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(model);
            }

            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public async Task<IActionResult> MeuPerfil()
        {
            return View();
        }

       
    }
}
