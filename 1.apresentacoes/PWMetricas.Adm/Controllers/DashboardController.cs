using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private readonly ILojaServico _lojaServico;
        private readonly IAtendimentoServico _atendimentoServico;
        private readonly IUsuarioServico _usuarioServico;

       
        public DashboardController(ILojaServico lojaServico, IUsuarioServico usuarioServico, IAtendimentoServico atendimentoServico)
        {

            _lojaServico = lojaServico;
            _usuarioServico = usuarioServico;
            _atendimentoServico = atendimentoServico;
        }



        public async Task<IActionResult> Index()
        {
            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();
            return View();
        }


        #region Graficos
        [HttpGet]
        public async Task<IActionResult> ListarOrigensPorStatus(int? mes, int? ano, int? loja, int status)
        {
            var lista = await _atendimentoServico.ObterOrigemGraficoStatusAsync(mes, ano, loja, status);
            return Json(new { data = lista });
        }

        [HttpGet]
        public async Task<IActionResult> ListarCanaisPorStatus(int? mes, int? ano, int? loja, int status)
        {
            var lista = await _atendimentoServico.ObterCanaisGraficoStatusAsync(mes, ano, loja, status);
            return Json(new { data = lista });
        }

        [HttpGet]
        public async Task<IActionResult> ListarVendedoresPorStatus(int? mes, int? ano, int? loja, int status)
        {
            var lista = await _atendimentoServico.ObterVendedorGraficoStatusAsync(mes, ano, loja, status);
            return Json(new { data = lista });
        }


        [HttpGet]
        public async Task<IActionResult> ListarCidadesPorStatus(int? mes, int? ano, int? loja, int status)
        {
            var lista = await _atendimentoServico.ObterCidadesGraficoStatusAsync(mes, ano, loja, status);
            return Json(new { data = lista });
        }

        #endregion
    }
}
