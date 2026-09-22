using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class ClienteCadastroViewModel
{
    [Required(ErrorMessage = "Informe o nome do cliente.")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CPF.")]
    [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
    [Display(Name = "CPF")]
    public string CPF { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
    public string? Telefone { get; set; }

    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [StringLength(250, ErrorMessage = "O endereço deve ter no máximo 250 caracteres.")]
    [Display(Name = "Endereço")]
    public string? Endereco { get; set; }

    [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
    public string? Cidade { get; set; }

    [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla do estado com 2 letras.")]
    public string? Estado { get; set; }

    [Required(ErrorMessage = "Selecione o status.")]
    [StringLength(10)]
    public string Status { get; set; } = "Ativo";
}
