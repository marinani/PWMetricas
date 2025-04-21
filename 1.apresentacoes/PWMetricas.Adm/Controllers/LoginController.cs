using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos.Usuario;

namespace PWMetricas.Adm.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public LoginController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Autenticar(LoginViewModel login)
        {
            return View();
        }
    }
}
