using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;
using OrdemGo.Services;
using OrdemGo.ViewModels;

namespace OrdemGo.Controllers;

public class ServicosController : Controller
{
    private readonly ServicoService _servicoService;

    public ServicosController(ServicoService servicoService)
    {
        _servicoService = servicoService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await MontarViewModelAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(
        [Bind(Prefix = "Cadastro")] ServicoCadastroViewModel cadastro)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _servicoService.CadastrarAsync(CriarServico(cadastro));
                TempData["MensagemSucesso"] = "Serviço cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível cadastrar o serviço. Verifique os dados e tente novamente.");
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
        [Bind(Prefix = "Edicao")] ServicoEdicaoViewModel edicao)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (!await _servicoService.EditarAsync(CriarServico(edicao, edicao.Id)))
                {
                    return NotFound();
                }

                TempData["MensagemSucesso"] = "Serviço atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível atualizar o serviço. Verifique os dados e tente novamente.");
            }
        }

        var viewModel = await MontarViewModelAsync();
        viewModel.Edicao = edicao;
        viewModel.AbrirModalServico = true;
        return View("Index", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            if (!await _servicoService.ExcluirAsync(id))
            {
                return NotFound();
            }

            TempData["MensagemSucesso"] = "Serviço excluído com sucesso.";
        }
        catch (DbUpdateException)
        {
            TempData["MensagemErro"] =
                "Não foi possível excluir o serviço porque existem registros vinculados a ele.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<ServicosIndexViewModel> MontarViewModelAsync()
    {
        return new ServicosIndexViewModel
        {
            Servicos = await _servicoService.ListarAsync()
        };
    }

    private static Servico CriarServico(ServicoCadastroViewModel dados, int id = 0)
    {
        return new Servico
        {
            Id = id,
            Descricao = dados.Descricao,
            TempoEstimadoHoras = dados.TempoEstimadoHoras,
            Valor = dados.Valor
        };
    }
}
