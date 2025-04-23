using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class VendedorController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IPerfilServico _perfilServico;

        public VendedorController(IUsuarioServico usuarioServico, IPerfilServico perfilServico)
        {
            _usuarioServico = usuarioServico;
            _perfilServico = perfilServico;
        }


        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var perfis = await _usuarioServico.ObterVendedoresPaginados(page, pageSize);

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
        public async Task<IActionResult> Cadastro(VendedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
                return View(model);
            }

            var resultado = await _usuarioServico.CadastrarVendedor(model);
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
            var usuario = await _usuarioServico.ObterVendedorPorId(id);
            if (usuario != null)
            {
                return View(usuario);
            }

            return RedirectToAction("Consulta");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(VendedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _usuarioServico.EditarVendedor(model);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                
                return View(model);
            }

            return RedirectToAction("Consulta");
        }
    }
}
