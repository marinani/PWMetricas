using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Perfil;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IPerfilServico _perfilServico;

        public UsuarioController(IUsuarioServico usuarioServico, IPerfilServico perfilServico)
        {
            _usuarioServico = usuarioServico;
            _perfilServico = perfilServico;
        }


        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var perfis = await _usuarioServico.ObterTodosPaginados(page, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Listagem", perfis);
            }

            return View(perfis);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastro()
        {
            ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(model);
            }

            var resultado = await _usuarioServico.CadastrarUsuario(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(model);
            }

            return RedirectToAction("Consulta");
        }
    }
}
