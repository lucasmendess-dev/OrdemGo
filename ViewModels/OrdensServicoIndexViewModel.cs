using OrdemGo.Models;

namespace OrdemGo.ViewModels;

public class OrdensServicoIndexViewModel
{
    public IReadOnlyList<OrdemServico> Ordens { get; set; } = [];

    public IReadOnlyList<Cliente> Clientes { get; set; } = [];

    public IReadOnlyList<Servico> ServicosDisponiveis { get; set; } = [];

    public OrdemServicoCadastroViewModel Cadastro { get; set; } = new();

    public OrdemServicoEdicaoViewModel Edicao { get; set; } = new();

    public bool AbrirModalCadastro { get; set; }

    public bool AbrirModalEdicao { get; set; }
}
