using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PWMetricas.WebApi.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet("protegido")]
        public IActionResult Protegido()
        {
            var userName = User.Identity?.Name;
            return Ok($"Olá {userName}, você acessou um endpoint protegido!");
        }
    }
}
