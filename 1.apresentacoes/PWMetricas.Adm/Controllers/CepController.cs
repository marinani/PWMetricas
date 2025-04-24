using Microsoft.AspNetCore.Mvc;
using PWMetricas.Aplicacao.Servicos;
using System.Threading.Tasks;

namespace PWMetricas.Adm.Controllers
{

    public class CepController : Controller
    {
        private readonly CepServico _cepServico;

        public CepController(CepServico cepServico)
        {
            _cepServico = cepServico;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                return BadRequest("CEP inválido.");
            }

            var resultado = await _cepServico.ConsultarCepAsync(cep);

            if (resultado == null)
            {
                return NotFound("CEP não encontrado.");
            }

            return Json(resultado);
        }
    }
}
