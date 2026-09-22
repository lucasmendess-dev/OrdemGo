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
        Normalizar(cliente);

        return _clienteDAO.AdicionarAsync(cliente);
    }

    public async Task<bool> EditarAsync(Cliente clienteAtualizado)
    {
        var cliente = await _clienteDAO.ObterPorIdAsync(clienteAtualizado.Id);
        if (cliente is null)
        {
            return false;
        }

        cliente.Nome = clienteAtualizado.Nome;
        cliente.CPF = clienteAtualizado.CPF;
        cliente.Telefone = clienteAtualizado.Telefone;
        cliente.Email = clienteAtualizado.Email;
        cliente.Endereco = clienteAtualizado.Endereco;
        cliente.Cidade = clienteAtualizado.Cidade;
        cliente.Estado = clienteAtualizado.Estado;
        cliente.Status = clienteAtualizado.Status;
        Normalizar(cliente);

        await _clienteDAO.SalvarAlteracoesAsync();
        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var cliente = await _clienteDAO.ObterPorIdAsync(id);
        if (cliente is null)
        {
            return false;
        }

        await _clienteDAO.ExcluirAsync(cliente);
        return true;
    }

    private static void Normalizar(Cliente cliente)
    {
        cliente.Nome = cliente.Nome.Trim();
        cliente.CPF = cliente.CPF.Trim();
        cliente.Telefone = LimparCampoOpcional(cliente.Telefone);
        cliente.Email = LimparCampoOpcional(cliente.Email);
        cliente.Endereco = LimparCampoOpcional(cliente.Endereco);
        cliente.Cidade = LimparCampoOpcional(cliente.Cidade);
        cliente.Estado = LimparCampoOpcional(cliente.Estado)?.ToUpperInvariant();
        cliente.Status = cliente.Status.Trim();
    }

    private static string? LimparCampoOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
    }
}
