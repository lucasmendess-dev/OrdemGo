using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;
using OrdemGo.Services;
using OrdemGo.ViewModels;

namespace OrdemGo.Controllers;

public class OrdensServicoController : Controller
{
    private readonly OrdemServicoService _ordemServicoService;
    private readonly ClienteService _clienteService;

    public OrdensServicoController(
        OrdemServicoService ordemServicoService,
        ClienteService clienteService)
    {
        _ordemServicoService = ordemServicoService;
        _clienteService = clienteService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await MontarViewModelAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(
        [Bind(Prefix = "Cadastro")] OrdemServicoCadastroViewModel cadastro)
    {
        if (cadastro.DataPrevisao.HasValue && cadastro.DataPrevisao < cadastro.DataAbertura)
        {
            ModelState.AddModelError(
                "Cadastro.DataPrevisao",
                "A previsão não pode ser anterior à abertura.");
        }

        if (cadastro.DataConclusao.HasValue && cadastro.DataConclusao < cadastro.DataAbertura)
        {
            ModelState.AddModelError(
                "Cadastro.DataConclusao",
                "A conclusão não pode ser anterior à abertura.");
        }

        if (ModelState.IsValid)
        {
            var ordem = new OrdemServico
            {
                ClienteId = cadastro.ClienteId,
                ResponsavelId = cadastro.ResponsavelId,
                Equipamento = cadastro.Equipamento,
                Modelo = cadastro.Modelo,
                DescricaoProblema = cadastro.DescricaoProblema,
                DiagnosticoTecnico = cadastro.DiagnosticoTecnico,
                SolucaoAplicada = cadastro.SolucaoAplicada,
                DataAbertura = cadastro.DataAbertura,
                DataPrevisao = cadastro.DataPrevisao,
                DataConclusao = cadastro.DataConclusao,
                Status = cadastro.Status,
                Prioridade = cadastro.Prioridade,
                Observacoes = cadastro.Observacoes,
                Desconto = cadastro.Desconto,
                Acrescimo = cadastro.Acrescimo
            };

            try
            {
                await _ordemServicoService.CadastrarAsync(ordem);
                TempData["MensagemSucesso"] = "Ordem de serviço cadastrada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível cadastrar a ordem. Verifique os dados e tente novamente.");
            }
        }

        var viewModel = await MontarViewModelAsync();
        viewModel.Cadastro = cadastro;
        viewModel.AbrirModalCadastro = true;
        return View("Index", viewModel);
    }

    private async Task<OrdensServicoIndexViewModel> MontarViewModelAsync()
    {
        return new OrdensServicoIndexViewModel
        {
            Ordens = await _ordemServicoService.ListarAsync(),
            Clientes = await _clienteService.ListarAsync()
        };
    }
}
