using Microsoft.AspNetCore.Mvc;

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
    }
}
