namespace OrdemGo.ViewModels;

public class HomeDashboardViewModel
{
    public int TotalClientes { get; set; }

    public int TotalOrdensServico { get; set; }

    public IReadOnlyList<StatusOrdemDashboardViewModel> OrdensPorStatus { get; set; } = [];
}
