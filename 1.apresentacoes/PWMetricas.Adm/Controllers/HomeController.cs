using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Adm.Models;
using Microsoft.AspNetCore.Authorization;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Dashboard;
using PWMetricas.Dominio.Filtros;

namespace PWMetricas.Adm.Controllers;


[Authorize]
public class HomeController : Controller
{

    private readonly ILojaServico _lojaServico;
    private readonly IAtendimentoServico _atendimentoServico;
    private readonly IUsuarioServico _usuarioServico;

    public HomeController(ILojaServico lojaServico, IUsuarioServico usuarioServico, IAtendimentoServico atendimentoServico)
    {

        _lojaServico = lojaServico;
        _usuarioServico = usuarioServico;
        _atendimentoServico = atendimentoServico;
    }

    public async Task<IActionResult> Index()
    {
        var perfil = User.Claims.FirstOrDefault(c => c.Type == "Perfil")?.Value;
        var usuarioLogadoId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

        ViewBag.Lojas = (await _lojaServico.ObterTodos())
                         .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


        var usuario = await _usuarioServico.ObterPorId(int.Parse(usuarioLogadoId));

        var modelo = new DashboardViewModel
        {
            LojaId = usuario.LojaId,
            Resultado = await CarregarResultado(int.Parse(usuarioLogadoId)),
            Tarefas = await CarregarTarefasDashboard(int.Parse(usuarioLogadoId))
        };


        if (perfil != null && perfil.Equals("Vendedor"))
        {
           
            modelo.Vendedor = await CarregarDashboardVendedorInicial(int.Parse(usuarioLogadoId));

           
        }
        else
        {
            

          
        }

        return View(modelo);
    }

  

    #region Métodos Privados
    private async Task CarregarCombosConsulta()
    {
        ViewBag.Lojas = (await _lojaServico.ObterTodos())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


        ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();

    }

    private async Task<ResultadoViewModel> CarregarResultado(int usuarioId)
    {
        var usuario = await _usuarioServico.ObterPorId(usuarioId);

        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        if (usuario.PerfilId == 3)
        {
            var resultado = new ResultadoViewModel
            {
                SomaAtendimento = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 1, usuario.LojaId),
                SomaOrcamento = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 2, usuario.LojaId),
                SomaVendido = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 3, usuario.LojaId),
                SomaNegociado = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 4, usuario.LojaId),
                SomaNaoResponde = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 5, usuario.LojaId)
            };

            return resultado;
        }
        else
        {
            var resultado = new ResultadoViewModel
            {
                SomaAtendimento = await _atendimentoServico.SomaTotalAtendimento(null, 1, null),
                SomaOrcamento = await _atendimentoServico.SomaTotalAtendimento(null, 2, null),
                SomaVendido = await _atendimentoServico.SomaTotalAtendimento(null, 3, null),
                SomaNegociado = await _atendimentoServico.SomaTotalAtendimento(null, 4, null),
                SomaNaoResponde = await _atendimentoServico.SomaTotalAtendimento(null, 5, null)
            };

            return resultado;

        }

    }
     
    private async Task<List<TarefasViewModel>> CarregarTarefasDashboard(int usuarioId)
    {
        var usuario = await _usuarioServico.ObterPorId(usuarioId);

        var IsVendedor = usuario.PerfilId == 3 ? true : false;

        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado.");
        }


        var filtro = new AtendimentoFiltro
        {
            LojaId = usuario.PerfilId == 3 ? usuario.LojaId : null,
            UsuarioId = usuario.PerfilId == 3 ? usuario.Id : null,
            DataAtual = DateTime.Now
        };

       return await _atendimentoServico.ObterTarefasPorFiltro(filtro);
    }

    private async Task<DashboardVendedorInicial> CarregarDashboardVendedorInicial(int usuarioId)
    {

        var usuario = await _usuarioServico.ObterPorId(usuarioId);

        if (usuario == null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        var vendedor = new DashboardVendedorInicial();


        vendedor.MinhasMetas = new Metas()
        {
            MetaMensalVendedor = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 3, usuario.LojaId),
            SuperMetaMensalVendedor = 0,
            MetaMensal = usuario.MetaMensal.HasValue ? usuario.MetaMensal.Value : 0,
            SuperMetaMensal = usuario.SuperMetaMensal.HasValue ? usuario.SuperMetaMensal.Value : 0

        };


        decimal metaMensal = vendedor.MinhasMetas.MetaMensal;
        decimal vendido = vendedor.MinhasMetas.MetaMensalVendedor;

        vendedor.MinhasMetas.ValorMetaPorcentagemMetaMensal = metaMensal > 0 ? Math.Round((vendido / metaMensal) * 100, 2) : 0;


        // Só calcula a porcentagem da super meta se a meta mensal foi atingida (100% ou mais)
        if (vendedor.MinhasMetas.ValorMetaPorcentagemMetaMensal >= 100 && vendedor.MinhasMetas.SuperMetaMensal > 0)
        {
            vendedor.MinhasMetas.ValorMetaPorcentagemSuperMetaMensal = Math.Round((vendido / vendedor.MinhasMetas.SuperMetaMensal) * 100, 2);
        }
        else
        {
            vendedor.MinhasMetas.ValorMetaPorcentagemSuperMetaMensal = 0;
        }

        return vendedor;

    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion


}
