using OrdemGo.Models;

namespace OrdemGo.ViewModels;

public class ClientesIndexViewModel
{
    public IReadOnlyList<Cliente> Clientes { get; set; } = [];

    public ClienteCadastroViewModel Cadastro { get; set; } = new();

    public bool AbrirModalCadastro { get; set; }
}
