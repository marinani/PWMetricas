using Microsoft.AspNetCore.Mvc;

namespace PWMetricas.Adm.Controllers
{
    public class AtendimentoController : Controller
    {
        public AtendimentoController() { }

        [HttpGet]
        [Route("Atendimento/Consulta")]
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        [Route("Atendimento/NovoAtendimento")]
        public IActionResult NovoAtendimento()
        {
            return View();
        }
    }
}
