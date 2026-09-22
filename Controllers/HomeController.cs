using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrdemGo.Models;
using OrdemGo.Services;
using OrdemGo.ViewModels;

namespace OrdemGo.Controllers;

public class HomeController : Controller
{
    private readonly ClienteService _clienteService;
    private readonly OrdemServicoService _ordemServicoService;

    public HomeController(
        ClienteService clienteService,
        OrdemServicoService ordemServicoService)
    {
        _clienteService = clienteService;
        _ordemServicoService = ordemServicoService;
    }

    public async Task<IActionResult> Index()
    {
        var totalClientes = await _clienteService.ContarAsync();
        var contagens = await _ordemServicoService.ContarPorStatusAsync();
        var status = new[]
        {
            "Aberta",
            "Em andamento",
            "Aguardando peças",
            "Concluída",
            "Cancelada"
        };

        var viewModel = new HomeDashboardViewModel
        {
            TotalClientes = totalClientes,
            TotalOrdensServico = contagens.Values.Sum(),
            OrdensPorStatus = status.Select(nome => new StatusOrdemDashboardViewModel
            {
                Nome = nome,
                Quantidade = contagens.GetValueOrDefault(nome)
            }).ToList()
        };

        return View(viewModel);
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
}
