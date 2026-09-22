using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdemGo.Models;
using OrdemGo.Services;
using OrdemGo.ViewModels;

namespace OrdemGo.Controllers;

public class ClientesController : Controller
{
    private readonly ClienteService _clienteService;

    public ClientesController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new ClientesIndexViewModel
        {
            Clientes = await _clienteService.ListarAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(
        [Bind(Prefix = "Cadastro")] ClienteCadastroViewModel cadastro)
    {
        if (ModelState.IsValid)
        {
            var cliente = new Cliente
            {
                Nome = cadastro.Nome,
                CPF = cadastro.CPF,
                Telefone = cadastro.Telefone,
                Email = cadastro.Email,
                Endereco = cadastro.Endereco,
                Cidade = cadastro.Cidade,
                Estado = cadastro.Estado,
                Status = cadastro.Status
            };

            try
            {
                await _clienteService.CadastrarAsync(cliente);
                TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível cadastrar o cliente. Verifique os dados e tente novamente.");
            }
        }

        var viewModel = new ClientesIndexViewModel
        {
            Clientes = await _clienteService.ListarAsync(),
            Cadastro = cadastro,
            AbrirModalCadastro = true
        };

        return View("Index", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(
        [Bind(Prefix = "Edicao")] ClienteEdicaoViewModel edicao)
    {
        if (ModelState.IsValid)
        {
            var cliente = new Cliente
            {
                Id = edicao.Id,
                Nome = edicao.Nome,
                CPF = edicao.CPF,
                Telefone = edicao.Telefone,
                Email = edicao.Email,
                Endereco = edicao.Endereco,
                Cidade = edicao.Cidade,
                Estado = edicao.Estado,
                Status = edicao.Status
            };

            try
            {
                if (!await _clienteService.EditarAsync(cliente))
                {
                    return NotFound();
                }

                TempData["MensagemSucesso"] = "Cliente atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Não foi possível atualizar o cliente. Verifique os dados e tente novamente.");
            }
        }

        var viewModel = new ClientesIndexViewModel
        {
            Clientes = await _clienteService.ListarAsync(),
            Edicao = edicao,
            AbrirModalCliente = true
        };

        return View("Index", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            if (!await _clienteService.ExcluirAsync(id))
            {
                return NotFound();
            }

            TempData["MensagemSucesso"] = "Cliente excluído com sucesso.";
        }
        catch (DbUpdateException)
        {
            TempData["MensagemErro"] =
                "Não foi possível excluir o cliente porque existem registros vinculados a ele.";
        }

        return RedirectToAction(nameof(Index));
    }
}
