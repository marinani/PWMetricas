using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos.Cliente;
using PWMetricas.Aplicacao.Servicos;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dominio.Entidades;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteServico _clienteServico;
        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        [HttpGet]
        public async Task<IActionResult> Consulta(int page = 1)
        {
            const int pageSize = 10;
            var perfis = await _clienteServico.ObterTodosPaginados(page, pageSize);

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
        public async Task<IActionResult> Cadastro(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            var resultado = await _clienteServico.Cadastrar(cliente);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", resultado.Erros));
                return View(cliente);
            }

            return RedirectToAction("Consulta");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid guid)
        {
            var cliente = await _clienteServico.ObterPorGuid(guid);

            if (cliente != null)
            {
                return View(cliente);
            }

            return RedirectToAction("Consulta");
        }
    }
}
