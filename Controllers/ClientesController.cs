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
    public async Task<IActionResult> Cadastrar(ClientesIndexViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var cliente = new Cliente
            {
                Nome = viewModel.Cadastro.Nome,
                CPF = viewModel.Cadastro.CPF,
                Telefone = viewModel.Cadastro.Telefone,
                Email = viewModel.Cadastro.Email,
                Endereco = viewModel.Cadastro.Endereco,
                Cidade = viewModel.Cadastro.Cidade,
                Estado = viewModel.Cadastro.Estado,
                Status = viewModel.Cadastro.Status
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

        viewModel.Clientes = await _clienteService.ListarAsync();
        viewModel.AbrirModalCadastro = true;

        return View("Index", viewModel);
    }
}
