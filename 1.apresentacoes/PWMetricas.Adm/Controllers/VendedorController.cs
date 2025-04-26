using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PWMetricas.Aplicacao.Servicos;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class VendedorController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IPerfilServico _perfilServico;
        private readonly ILojaServico _lojaServico;

        public VendedorController(IUsuarioServico usuarioServico, IPerfilServico perfilServico, ILojaServico lojaServico)
        {
            _usuarioServico = usuarioServico;
            _perfilServico = perfilServico;
            _lojaServico = lojaServico;
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
            await CarregarCombos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(VendedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCombos();
                return View(model);
            }

            var resultado = await _usuarioServico.CadastrarVendedor(model);
            if (!resultado.Sucesso)
            {
                await CarregarCombos();
                TempData["MensagemErro"] = string.Join(", ", resultado.Erros);
                return View(model);
            }

            TempData["MensagemSucesso"] = "Vendedor cadastrado com sucesso!";
            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioServico.ObterVendedorPorId(id);
            await CarregarCombos();
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
                await CarregarCombos();
                return View(model);
            }

            var resultado = await _usuarioServico.EditarVendedor(model);
            if (!resultado.Sucesso)
            {
                await CarregarCombos();
                TempData["MensagemErro"] = string.Join(", ", resultado.Erros);
                
                return View(model);
            }

            TempData["MensagemSucesso"] = "Vendedor atualizado com sucesso!";
            return RedirectToAction("Consulta");
        }


        private async Task CarregarCombos()
        {
            ViewBag.Perfis = new SelectList(await _perfilServico.ObterTodos(), "Id", "Nome");
            ViewBag.Lojas = (await _lojaServico.ObterTodos())
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = $"{c.NomeFantasia} - {c.CNPJ}"
         })
         .ToList();
        }
    }
}
