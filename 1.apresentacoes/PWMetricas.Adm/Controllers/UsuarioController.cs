using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioServico.ObterPorId(id);
            if (usuario != null)
            {
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(model);
            }

            var resultado = await _usuarioServico.EditarUsuario(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(model);
            }

            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public IActionResult AlterarSenha()
        {
            //Obter o ID do usuário logado
            var id = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Criar o modelo de alteração de senha
            var modelo = new UsuarioSenhaViewModel
            {
                Id = int.Parse(id)
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(UsuarioSenhaViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var resultado = await _usuarioServico.AlterarSenha(modelo);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(modelo);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
