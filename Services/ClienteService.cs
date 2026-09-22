using OrdemGo.DAO;
using OrdemGo.Models;

namespace OrdemGo.Services;

public class ClienteService
{
    private readonly ClienteDAO _clienteDAO;

    public ClienteService(ClienteDAO clienteDAO)
    {
        _clienteDAO = clienteDAO;
    }

    public Task<IReadOnlyList<Cliente>> ListarAsync()
    {
        return _clienteDAO.ListarAsync();
    }

    public Task CadastrarAsync(Cliente cliente)
    {
        cliente.Nome = cliente.Nome.Trim();
        cliente.CPF = cliente.CPF.Trim();
        cliente.Telefone = LimparCampoOpcional(cliente.Telefone);
        cliente.Email = LimparCampoOpcional(cliente.Email);
        cliente.Endereco = LimparCampoOpcional(cliente.Endereco);
        cliente.Cidade = LimparCampoOpcional(cliente.Cidade);
        cliente.Estado = LimparCampoOpcional(cliente.Estado)?.ToUpperInvariant();
        cliente.Status = cliente.Status.Trim();

        return _clienteDAO.AdicionarAsync(cliente);
    }

    private static string? LimparCampoOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
    }
}
