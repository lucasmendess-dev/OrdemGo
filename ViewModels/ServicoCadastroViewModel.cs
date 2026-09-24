using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class ServicoCadastroViewModel
{
    [Required(ErrorMessage = "Informe a descrição do serviço.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Range(typeof(decimal), "0,25", "8760,00", ErrorMessage = "Informe um tempo estimado entre 0,25 e 8.760 horas.")]
    [Display(Name = "Tempo estimado (horas)")]
    public decimal TempoEstimadoHoras { get; set; }

    [Range(typeof(decimal), "0,00", "9999999999999999,99", ErrorMessage = "Informe um valor válido.")]
    [Display(Name = "Valor")]
    public decimal Valor { get; set; }
}
