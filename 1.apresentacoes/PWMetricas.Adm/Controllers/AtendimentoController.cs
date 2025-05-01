using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using PWMetricas.Dominio.Entidades;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Adm.Controllers
{
    [Authorize]
    public class AtendimentoController : Controller
    {
        private readonly IOrigemServico _origemServico;
        private readonly ITamanhoServico _tamanhoServico;
        private readonly ICanalServico _canalServico;
        private readonly IProdutoServico _produtoServico;
        private readonly IClienteServico _clienteServico;
        private readonly ILojaServico _lojaServico;
        private readonly IStatusAtendimentoServico _statusServico;
        private readonly IAtendimentoServico _atendimentoServico;
        private readonly IUsuarioServico _usuarioServico;
        public AtendimentoController(IOrigemServico origemServico, ITamanhoServico tamanhoServico,
            ICanalServico canalServico, IProdutoServico produtoServico, IClienteServico clienteServico,
            ILojaServico lojaServico, IStatusAtendimentoServico statusServico,
            IUsuarioServico usuarioServico, IAtendimentoServico atendimentoServico)
        {
            _origemServico = origemServico;
            _tamanhoServico = tamanhoServico;
            _canalServico = canalServico;
            _produtoServico = produtoServico;
            _clienteServico = clienteServico;
            _lojaServico = lojaServico;
            _statusServico = statusServico;
            _usuarioServico = usuarioServico;
            _atendimentoServico = atendimentoServico;
        }

        [HttpGet]
        [Route("Atendimento/Consulta")]
        public async Task<IActionResult> Consulta()
        {
            await CarregarCombosConsulta();
            var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
            var usuarioLogadoId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (perfil != null && perfil.Equals("Vendedor"))
            {
                var usuario = await _usuarioServico.ObterPorId(int.Parse(usuarioLogadoId));
                var consulta = new AtendimentoConsultaViewModel
                {
                    IsVendedor = true,
                    LojaId = usuario.LojaId
                };

                return View(consulta);
            }
            else
            {
                return View();
            }
        }



        [HttpGet]
        [Route("Atendimento/ListaAtendimento")]
        public async Task<IActionResult> ListaAtendimento(AtendimentoFiltro filtro, int pagina = 1)
        {
            const int pageSize = 10; // Número de itens por página
            filtro.StatusAtendimentoId = 1; // Status 1
            filtro = await AtendimentoFiltro(filtro);
            var resultadoPaginado = await _atendimentoServico.ObterAtendimentosPaginados(pagina, pageSize, filtro);

            var viewModel = new AtendimentoConsultaViewModel
            {
                Filtro = filtro,
                Resultados = resultadoPaginado?.Dados ?? new List<AtendimentoListaViewModel>(), // Evita null
                PaginaAtual = resultadoPaginado?.PaginaAtual ?? 1,
                TotalPaginas = resultadoPaginado?.TotalPaginas ?? 1,
                TotalRegistros = resultadoPaginado?.TotalRegistros ?? 0,
                ValorPedido = resultadoPaginado?.SomaTotal ?? "0,00"
            };

            return PartialView("_ListaAtendimento", viewModel);
        }

        [HttpGet]
        [Route("Atendimento/ListaOrcamento")]
        public async Task<IActionResult> ListaOrcamento(AtendimentoFiltro filtro, int pagina = 1)
        {
            const int pageSize = 10; // Número de itens por página
            filtro.StatusAtendimentoId = 2; // Status 2
            filtro = await AtendimentoFiltro(filtro);
            var resultadoPaginado = await _atendimentoServico.ObterAtendimentosPaginados(pagina, pageSize, filtro);

            var viewModel = new AtendimentoConsultaViewModel
            {
                Filtro = filtro,
                Resultados = resultadoPaginado?.Dados ?? new List<AtendimentoListaViewModel>(), // Evita null
                PaginaAtual = resultadoPaginado?.PaginaAtual ?? 1,
                TotalPaginas = resultadoPaginado?.TotalPaginas ?? 1,
                TotalRegistros = resultadoPaginado?.TotalRegistros ?? 0,
                ValorPedido = resultadoPaginado?.SomaTotal ?? "0,00"
            };

            return PartialView("_ListaOrcamento", viewModel);
        }

        [HttpGet]
        [Route("Atendimento/ListaVendido")]
        public async Task<IActionResult> ListaVendido(AtendimentoFiltro filtro, int pagina = 1)
        {
            const int pageSize = 10; // Número de itens por página
            filtro.StatusAtendimentoId = 3;
            filtro = await AtendimentoFiltro(filtro);
            var resultadoPaginado = await _atendimentoServico.ObterAtendimentosPaginados(pagina, pageSize, filtro);

            var viewModel = new AtendimentoConsultaViewModel
            {
                Filtro = filtro,
                Resultados = resultadoPaginado?.Dados ?? new List<AtendimentoListaViewModel>(), // Evita null
                PaginaAtual = resultadoPaginado?.PaginaAtual ?? 1,
                TotalPaginas = resultadoPaginado?.TotalPaginas ?? 1,
                TotalRegistros = resultadoPaginado?.TotalRegistros ?? 0,
                ValorPedido = resultadoPaginado?.SomaTotal ?? "0,00"
            };

            return PartialView("_ListaVendido", viewModel);
        }

        [HttpGet]
        [Route("Atendimento/ListaNegociado")]
        public async Task<IActionResult> ListaNegociado(AtendimentoFiltro filtro, int pagina = 1)
        {
            const int pageSize = 10; // Número de itens por página
            filtro.StatusAtendimentoId = 4;
            filtro = await AtendimentoFiltro(filtro);
            var resultadoPaginado = await _atendimentoServico.ObterAtendimentosPaginados(pagina, pageSize, filtro);

            var viewModel = new AtendimentoConsultaViewModel
            {
                Filtro = filtro,
                Resultados = resultadoPaginado?.Dados ?? new List<AtendimentoListaViewModel>(), // Evita null
                PaginaAtual = resultadoPaginado?.PaginaAtual ?? 1,
                TotalPaginas = resultadoPaginado?.TotalPaginas ?? 1,
                TotalRegistros = resultadoPaginado?.TotalRegistros ?? 0,
                ValorPedido = resultadoPaginado?.SomaTotal ?? "0,00"
            };

            return PartialView("_ListaNegociado", viewModel);
        }

        [HttpGet]
        [Route("Atendimento/ListaNaoResponde")]
        public async Task<IActionResult> ListaNaoResponde(AtendimentoFiltro filtro, int pagina = 1)
        {
            const int pageSize = 10; // Número de itens por página
            filtro.StatusAtendimentoId = 5;
            filtro = await AtendimentoFiltro(filtro);
            var resultadoPaginado = await _atendimentoServico.ObterAtendimentosPaginados(pagina, pageSize, filtro);

            var viewModel = new AtendimentoConsultaViewModel
            {
                Filtro = filtro,
                Resultados = resultadoPaginado?.Dados ?? new List<AtendimentoListaViewModel>(), // Evita null
                PaginaAtual = resultadoPaginado?.PaginaAtual ?? 1,
                TotalPaginas = resultadoPaginado?.TotalPaginas ?? 1,
                TotalRegistros = resultadoPaginado?.TotalRegistros ?? 0,
                ValorPedido = resultadoPaginado?.SomaTotal ?? "0,00"
            };

            return PartialView("_ListaNaoResponde", viewModel);
        }


        [HttpGet]
        [Route("Atendimento/NovoAtendimento")]
        public async Task<IActionResult> NovoAtendimento()
        {
            await CarregarCombos();
            //Obter o ID do usuário logado
            var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
            var usuarioLogadoId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (perfil != null && perfil.Equals("Vendedor"))
            {
                var usuario = await _usuarioServico.ObterPorId(int.Parse(usuarioLogadoId));


                var modelo = new AtendimentoViewModel
                {
                    UsuarioId = usuarioLogadoId.Equals("0") ? 0 : int.Parse(usuarioLogadoId),
                    LojaId = usuario.LojaId,
                    IsVendedor = true

                };


                return View(modelo);
            }
            else
            {
                return View();
            }

        }



        [HttpPost]
        [Route("Atendimento/NovoAtendimento")]
        public async Task<IActionResult> NovoAtendimento(AtendimentoViewModel atendimento)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { sucesso = false, erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var resultado = await _atendimentoServico.Cadastrar(atendimento);
            if (!resultado.Sucesso)
            {
                return Ok(new { sucesso = false, erros = resultado.Erros });
            }

            return Ok(new { sucesso = true, mensagem = "Sucesso ao cadastrar atendimento." });

        }

        [HttpGet]
        [Route("Atendimento/Editar")]
        public async Task<IActionResult> Editar(Guid guid)
        {
            var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
            var atendimento = await _atendimentoServico.ObterPorGuid(guid);

            if (atendimento != null)
            {
                await CarregarCombosEdicao(atendimento);

                if (perfil != null && perfil.Equals("Vendedor"))
                {
                    atendimento.IsVendedor = true;
                }
                return View(atendimento);
            }

            return RedirectToAction("Consulta");

        }

        [HttpPost]
        [Route("Atendimento/Editar")]
        public async Task<IActionResult> Editar(AtendimentoViewModel atendimento)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { sucesso = false, erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var resultado = await _atendimentoServico.Atualizar(atendimento);
            if (!resultado.Sucesso)
            {
                return Ok(new { sucesso = false, erros = resultado.Erros });
            }

            return Ok(new { sucesso = true, mensagem = "Sucesso ao atualizar atendimento." });

        }

        [HttpGet]
        [Route("Atendimento/Visualizar")]
        public async Task<IActionResult> Visualizar(Guid guid)
        {
            var atendimento = await _atendimentoServico.ObterPorGuid(guid);

            if (atendimento != null)
            {
                await CarregarCombosEdicao(atendimento);

                return View(atendimento);
            }

            return RedirectToAction("Consulta");

        }


        #region Private Methods
        private async Task CarregarCombos()
        {
            ViewBag.Canais = (await _canalServico.ObterTodos())
                        .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Origens = (await _origemServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Produtos = (await _produtoServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Tamanhos = (await _tamanhoServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Status = (await _statusServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();


            ViewBag.Clientes = (await _clienteServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome + " - " + c.Telefone }).ToList();


            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


            ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();
        }

        private async Task CarregarCombosConsulta()
        {
            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


            ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();

        }

        private async Task<AtendimentoFiltro> AtendimentoFiltro(AtendimentoFiltro filtro)
        {
            var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
            var usuarioLogadoId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (perfil != null && perfil.Equals("Vendedor"))
            {
                var usuario = await _usuarioServico.ObterPorId(int.Parse(usuarioLogadoId));
                filtro.UsuarioId = usuario.Id;
                filtro.LojaId = usuario.LojaId;
                filtro.IsVendedor = true;
            }
            return filtro;
        }

        private async Task CarregarCombosEdicao(AtendimentoViewModel atendimento)
        {

            ViewBag.Canais = (await _canalServico.ObterTodos())
                        .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Origens = (await _origemServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Produtos = (await _produtoServico.ObterTodos())
                         .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Tamanhos = (await _tamanhoServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();

            ViewBag.Status = (await _statusServico.ObterTodos())
                          .Select(c => new { Id = c.Id.ToString(), Nome = c.Nome, CorHex = c.CorHex }).ToList();


            ViewBag.Clientes = (await _clienteServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome + " - " + c.Telefone }).ToList();


            ViewBag.Lojas = (await _lojaServico.ObterTodos())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


            ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                          .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();
        }
        #endregion
    }
}
