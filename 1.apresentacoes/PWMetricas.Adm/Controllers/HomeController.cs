using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PWMetricas.Adm.Models;
using Microsoft.AspNetCore.Authorization;
using PWMetricas.Aplicacao.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task CarregarCombosConsulta()
    {
        ViewBag.Lojas = (await _lojaServico.ObterTodos())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.NomeFantasia + " - " + c.CNPJ }).ToList();


        ViewBag.Vendedores = (await _usuarioServico.ListarVendedores())
                      .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome }).ToList();

    }
}
