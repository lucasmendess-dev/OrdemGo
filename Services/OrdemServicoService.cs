using OrdemGo.DAO;
using OrdemGo.Models;

namespace OrdemGo.Services;

public class OrdemServicoService
{
    private readonly OrdemServicoDAO _ordemServicoDAO;

    public OrdemServicoService(OrdemServicoDAO ordemServicoDAO)
    {
        _ordemServicoDAO = ordemServicoDAO;
    }

    public Task<IReadOnlyList<OrdemServico>> ListarAsync()
    {
        return _ordemServicoDAO.ListarAsync();
    }

    public Task<IReadOnlyDictionary<string, int>> ContarPorStatusAsync()
    {
        return _ordemServicoDAO.ContarPorStatusAsync();
    }

    public Task CadastrarAsync(OrdemServico ordem)
    {
        ordem.Equipamento = ordem.Equipamento.Trim();
        ordem.Modelo = ordem.Modelo.Trim();
        ordem.DescricaoProblema = ordem.DescricaoProblema.Trim();
        ordem.DiagnosticoTecnico = LimparCampoOpcional(ordem.DiagnosticoTecnico);
        ordem.SolucaoAplicada = LimparCampoOpcional(ordem.SolucaoAplicada);
        ordem.Status = ordem.Status.Trim();
        ordem.Prioridade = ordem.Prioridade.Trim();
        ordem.Observacoes = LimparCampoOpcional(ordem.Observacoes);

        return _ordemServicoDAO.AdicionarAsync(ordem);
    }

    private static string? LimparCampoOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
    }
}
