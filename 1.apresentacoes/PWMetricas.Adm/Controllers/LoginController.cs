using System.Security.Claims;
using Google.Authenticator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Modelos.Usuario;
using PWMetricas.Aplicacao.Servicos.Interfaces;

namespace PWMetricas.Adm.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly ILogger<HomeController> _logger;
        private readonly TwoFactorAuthenticator _authenticator;

        public LoginController(IUsuarioServico usuarioServico, ILogger<HomeController> logger)
        {
            _usuarioServico = usuarioServico;
            _logger = logger;
            _authenticator = new TwoFactorAuthenticator();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", login);
            }

            // Verifica as credenciais do usuário
            var usuario = await _usuarioServico.AutenticarUsuario(login.Email, login.Senha);
            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                return View("Index", login);
            }

            // Cria as claims do usuário
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.PerfilNome), // Adiciona o perfil como Role
                new Claim("Id", usuario.Id.ToString()),  
                new Claim("Perfil", usuario.PerfilNome),      // Perfil do usuário
                new Claim("PerfilId", usuario.PerfilId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            // Autentica o usuário
            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "Home");
        }





        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Limpar os dados da sessão
            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }


        [HttpGet]
        [Route("AcessoNegado")]
        public IActionResult AcessoNegado()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
