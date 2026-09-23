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
        ValidarDatas(cadastro, "Cadastro");

        if (ModelState.IsValid)
        {
            try
            {
                await _ordemServicoService.CadastrarAsync(CriarOrdem(cadastro));
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(
        [Bind(Prefix = "Edicao")] OrdemServicoEdicaoViewModel edicao)
    {
        ValidarDatas(edicao, "Edicao");

        if (ModelState.IsValid)
        {
            try
            {
                if (!await _ordemServicoService.EditarAsync(CriarOrdem(edicao, edicao.Id)))
                {
                    return NotFound();
                }

                TempData["MensagemSucesso"] = "Ordem de serviço atualizada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível atualizar a ordem. Verifique os dados e tente novamente.");
            }
        }

        var viewModel = await MontarViewModelAsync();
        viewModel.Edicao = edicao;
        viewModel.AbrirModalEdicao = true;
        return View("Index", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Finalizar(int id)
    {
        var resultado = await _ordemServicoService.FinalizarAsync(id);
        switch (resultado)
        {
            case FinalizacaoOrdemResultado.NaoEncontrada:
                return NotFound();
            case FinalizacaoOrdemResultado.Indisponivel:
                TempData["MensagemErro"] = "Esta ordem já está concluída ou cancelada.";
                break;
            default:
                TempData["MensagemSucesso"] = "Ordem de serviço finalizada com sucesso.";
                break;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Imprimir(int id)
    {
        var ordem = await _ordemServicoService.ObterDetalhesAsync(id);
        return ordem is null ? NotFound() : View(ordem);
    }

    private async Task<OrdensServicoIndexViewModel> MontarViewModelAsync()
    {
        return new OrdensServicoIndexViewModel
        {
            Ordens = await _ordemServicoService.ListarAsync(),
            Clientes = await _clienteService.ListarAsync()
        };
    }

    private void ValidarDatas(OrdemServicoCadastroViewModel dados, string prefixo)
    {
        if (dados.DataPrevisao.HasValue && dados.DataPrevisao < dados.DataAbertura)
        {
            ModelState.AddModelError(
                $"{prefixo}.DataPrevisao",
                "A previsão não pode ser anterior à abertura.");
        }

        if (dados.DataConclusao.HasValue && dados.DataConclusao < dados.DataAbertura)
        {
            ModelState.AddModelError(
                $"{prefixo}.DataConclusao",
                "A conclusão não pode ser anterior à abertura.");
        }
    }

    private static OrdemServico CriarOrdem(
        OrdemServicoCadastroViewModel dados,
        int id = 0)
    {
        return new OrdemServico
        {
            Id = id,
            ClienteId = dados.ClienteId,
            ResponsavelId = dados.ResponsavelId,
            Equipamento = dados.Equipamento,
            Modelo = dados.Modelo,
            DescricaoProblema = dados.DescricaoProblema,
            DiagnosticoTecnico = dados.DiagnosticoTecnico,
            SolucaoAplicada = dados.SolucaoAplicada,
            DataAbertura = dados.DataAbertura,
            DataPrevisao = dados.DataPrevisao,
            DataConclusao = dados.DataConclusao,
            Status = dados.Status,
            Prioridade = dados.Prioridade,
            Observacoes = dados.Observacoes,
            ValorServico = dados.ValorServico,
            ValorPecas = dados.ValorPecas
        };
    }
}
