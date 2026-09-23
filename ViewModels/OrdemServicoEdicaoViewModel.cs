using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class OrdemServicoEdicaoViewModel : OrdemServicoCadastroViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Ordem de serviço inválida.")]
    public int Id { get; set; }
}
