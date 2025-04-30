using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Adm.Models;
using Microsoft.AspNetCore.Authorization;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWMetricas.Aplicacao.Modelos.Atendimento;
using PWMetricas.Aplicacao.Modelos.Dashboard;

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
        if (perfil != null && perfil.Equals("Vendedor"))
        {
            var usuario = await _usuarioServico.ObterPorId(int.Parse(usuarioLogadoId));

            var modelo = new DashboardViewModel
            {
                LojaId = usuario.LojaId,
                Vendedor = await CarregarDashboardVendedorInicial(usuario.Id)
            };

            return View(modelo);
        }
        else
        {
            return View();
        }
    }

   
    private async Task CarregarCombosConsulta()
    {
        ViewBag.Lojas = (await _lojaServico.ObterTodos())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


        ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();

    }

    private async Task<DashboardVendedorInicial> CarregarDashboardVendedorInicial(int usuarioId)
    {

        var usuario = await _usuarioServico.ObterPorId(usuarioId);

        if(usuario == null)
        {
            throw new Exception("Usuário não encontrado.");
        }

        var vendedor = new DashboardVendedorInicial();
        vendedor.NomeUsuario = usuario.Nome;
        vendedor.Tarefas = new List<Tarefas>();
        
        vendedor.Resultado = new ResultadoViewModel()
        {
            SomaAtendimento = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 1, usuario.LojaId),
            SomaOrcamento = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 2, usuario.LojaId),
            SomaVendido = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 3, usuario.LojaId),
            SomaNegociado = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 4, usuario.LojaId),
            SomaNaoResponde = await _atendimentoServico.SomaTotalAtendimento(usuarioId, 5, usuario.LojaId)
        };

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


      

        //var filtro = new AtendimentoFiltro
        //{
        //    LojaId = lojaId,
        //    UsuarioId = usuarioId
        //};
        //var atendimentos = await _atendimentoServico.ObterAtendimentos(filtro);
        //vendedor.Tarefas.AddRange(atendimentos.Select(a => new Tarefas
        //{
        //    Guid = a.Guid,
        //    Cliente = a.Cliente.Nome,
        //    Data = a.Data.ToShortDateString(),
        //    DataRetorno = a.DataRetorno.HasValue ? a.DataRetorno.Value.ToShortDateString() : "",
        //    ValorPedido = a.ValorPedido.ToString("C"),
        //    SomaTotal = a.SomaTotal.ToString("C")
        //}));
        return vendedor;

    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
