using OrdemGo.DAO;
using OrdemGo.Models;

namespace OrdemGo.Services;

public class ServicoService
{
    private readonly ServicoDAO _servicoDAO;

    public ServicoService(ServicoDAO servicoDAO)
    {
        _servicoDAO = servicoDAO;
    }

    public Task<IReadOnlyList<Servico>> ListarAsync()
    {
        return _servicoDAO.ListarAsync();
    }

    public Task CadastrarAsync(Servico servico)
    {
        Normalizar(servico);
        return _servicoDAO.AdicionarAsync(servico);
    }

    public async Task<bool> EditarAsync(Servico servicoAtualizado)
    {
        var servico = await _servicoDAO.ObterPorIdAsync(servicoAtualizado.Id);
        if (servico is null)
        {
            return false;
        }

        servico.Descricao = servicoAtualizado.Descricao;
        servico.TempoEstimadoHoras = servicoAtualizado.TempoEstimadoHoras;
        servico.Valor = servicoAtualizado.Valor;
        Normalizar(servico);

        await _servicoDAO.SalvarAlteracoesAsync();
        return true;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var servico = await _servicoDAO.ObterPorIdAsync(id);
        if (servico is null)
        {
            return false;
        }

        await _servicoDAO.ExcluirAsync(servico);
        return true;
    }

    private static void Normalizar(Servico servico)
    {
        servico.Descricao = servico.Descricao.Trim();
    }
}
