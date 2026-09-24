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

    public Task CadastrarAsync(
        OrdemServico ordem,
        IReadOnlyCollection<Servico> servicosSelecionados)
    {
        Normalizar(ordem);
        DefinirServicos(ordem, servicosSelecionados);

        return _ordemServicoDAO.AdicionarAsync(ordem);
    }

    public Task<OrdemServico?> ObterDetalhesAsync(int id)
    {
        return _ordemServicoDAO.ObterDetalhesAsync(id);
    }

    public async Task<bool> EditarAsync(
        OrdemServico ordemAtualizada,
        IReadOnlyCollection<Servico> servicosSelecionados)
    {
        var ordem = await _ordemServicoDAO.ObterPorIdAsync(ordemAtualizada.Id);
        if (ordem is null)
        {
            return false;
        }

        ordem.ClienteId = ordemAtualizada.ClienteId;
        ordem.ResponsavelId = ordemAtualizada.ResponsavelId;
        ordem.Equipamento = ordemAtualizada.Equipamento;
        ordem.Modelo = ordemAtualizada.Modelo;
        ordem.DescricaoProblema = ordemAtualizada.DescricaoProblema;
        ordem.DiagnosticoTecnico = ordemAtualizada.DiagnosticoTecnico;
        ordem.SolucaoAplicada = ordemAtualizada.SolucaoAplicada;
        ordem.DataAbertura = ordemAtualizada.DataAbertura;
        ordem.DataPrevisao = ordemAtualizada.DataPrevisao;
        ordem.DataConclusao = ordemAtualizada.DataConclusao;
        ordem.Status = ordemAtualizada.Status;
        ordem.Prioridade = ordemAtualizada.Prioridade;
        ordem.Observacoes = ordemAtualizada.Observacoes;
        ordem.ValorPecas = ordemAtualizada.ValorPecas;
        DefinirServicos(ordem, servicosSelecionados);
        Normalizar(ordem);

        await _ordemServicoDAO.SalvarAlteracoesAsync();
        return true;
    }

    public async Task<FinalizacaoOrdemResultado> FinalizarAsync(int id)
    {
        var ordem = await _ordemServicoDAO.ObterPorIdAsync(id);
        if (ordem is null)
        {
            return FinalizacaoOrdemResultado.NaoEncontrada;
        }

        if (string.Equals(ordem.Status, "Concluída", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(ordem.Status, "Cancelada", StringComparison.OrdinalIgnoreCase))
        {
            return FinalizacaoOrdemResultado.Indisponivel;
        }

        ordem.Status = "Concluída";
        ordem.DataConclusao = DateTime.Now;
        await _ordemServicoDAO.SalvarAlteracoesAsync();
        return FinalizacaoOrdemResultado.Concluida;
    }

    private static void Normalizar(OrdemServico ordem)
    {
        ordem.Equipamento = ordem.Equipamento.Trim();
        ordem.Modelo = ordem.Modelo.Trim();
        ordem.DescricaoProblema = ordem.DescricaoProblema.Trim();
        ordem.DiagnosticoTecnico = LimparCampoOpcional(ordem.DiagnosticoTecnico);
        ordem.SolucaoAplicada = LimparCampoOpcional(ordem.SolucaoAplicada);
        ordem.Status = ordem.Status.Trim();
        ordem.Prioridade = ordem.Prioridade.Trim();
        ordem.Observacoes = LimparCampoOpcional(ordem.Observacoes);
    }

    private static void DefinirServicos(
        OrdemServico ordem,
        IReadOnlyCollection<Servico> servicosSelecionados)
    {
        ordem.Servicos.Clear();

        foreach (var servico in servicosSelecionados)
        {
            ordem.Servicos.Add(new OrdemServicoServico
            {
                ServicoId = servico.Id,
                Valor = servico.Valor
            });
        }

        ordem.ValorServico = servicosSelecionados.Sum(servico => servico.Valor);
    }

    private static string? LimparCampoOpcional(string? valor)
    {
        return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
    }
}
