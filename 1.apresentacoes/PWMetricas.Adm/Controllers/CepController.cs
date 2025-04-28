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

            cep = Dominio.Utils.Geral.ApenasNumeros(cep);
            var resultado = await _cepServico.ConsultarCepAsync(cep);

            if (resultado == null)
            {
                return NotFound("CEP não encontrado.");
            }

            return Json(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarCidadePorEstado(string siglaUF)
        {
            if (string.IsNullOrWhiteSpace(siglaUF))
            {
                return BadRequest("Estado inválido.");
            }

           
            var resultado = await _cepServico.ConsultarCidadePorEstadoAsync(siglaUF);

            if (resultado == null)
            {
                return NotFound("Municípios não encontrados.");
            }

            return Json(resultado);
        }
    }
}
