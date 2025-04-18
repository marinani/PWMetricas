using Microsoft.AspNetCore.Mvc;

namespace PWMetricas.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
