using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWMetricas.Dados;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dominio.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PWMetricas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {

        private readonly PwMetricasDbContext _context;

        public PerfilController(PwMetricasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _context.Perfil.ToListAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _context.Perfil.FindAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Perfil profile)
        {
            _context.Perfil.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Perfil profile)
        {
            var existing = await _context.Perfil.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Nome = profile.Nome;
            existing.Ativo = profile.Ativo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _context.Perfil.FindAsync(id);
            if (profile == null) return NotFound();

            _context.Perfil.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
