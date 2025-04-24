using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class LojaController : Controller
    {
        public LojaController()
        {
        }

        [HttpGet]
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }
    }
}
