using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class ServicoEdicaoViewModel : ServicoCadastroViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Serviço inválido.")]
    public int Id { get; set; }
}
