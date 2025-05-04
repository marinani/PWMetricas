using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dominio.Utils;

namespace PWMetricas.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly PwMetricasDbContext _context;

        public UsuarioController(PwMetricasDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("/GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            return Ok("Usuário autenticado com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Usuario.Include(u => u.Perfil).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Usuario.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario user)
        {
            user.Senha = CriptoUtil.Encrypt(user.Senha!);
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario user)
        {
            var existing = await _context.Usuario.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Nome = user.Nome;
            if (!string.IsNullOrEmpty(user.Senha))
                existing.Senha = CriptoUtil.Encrypt(user.Senha);
            existing.Ativo = user.Ativo;
            existing.PerfilId = user.PerfilId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Usuario.FindAsync(id);
            if (user == null) return NotFound();

            _context.Usuario.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
