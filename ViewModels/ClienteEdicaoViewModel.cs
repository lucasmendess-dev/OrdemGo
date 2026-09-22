using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class ClienteEdicaoViewModel : ClienteCadastroViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Cliente inválido.")]
    public int Id { get; set; }
}
