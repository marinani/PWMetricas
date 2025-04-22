using Microsoft.AspNetCore.Mvc;

namespace PWMetricas.Adm.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
