using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados;
using PWMetricas.Dominio.Models;
using PWMetricas.Dominio.Utils;

namespace PWMetricas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly PwMetricasDbContext _context;

        public UsuarioController(PwMetricasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Usuarios.Include(u => u.Perfil).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Usuarios.Include(u => u.Perfil).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario user)
        {
            user.Senha = CriptoUtil.Encrypt(user.Senha!);
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario user)
        {
            var existing = await _context.Usuarios.FindAsync(id);
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
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return NotFound();

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
