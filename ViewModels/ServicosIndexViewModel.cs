using OrdemGo.Models;

namespace OrdemGo.ViewModels;

public class ServicosIndexViewModel
{
    public IReadOnlyList<Servico> Servicos { get; set; } = [];

    public ServicoCadastroViewModel Cadastro { get; set; } = new();

    public ServicoEdicaoViewModel Edicao { get; set; } = new();

    public bool AbrirModalCadastro { get; set; }

    public bool AbrirModalServico { get; set; }
}
