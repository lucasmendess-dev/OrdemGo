using System.ComponentModel.DataAnnotations;

namespace OrdemGo.ViewModels;

public class OrdemServicoCadastroViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Selecione o cliente.")]
    [Display(Name = "Cliente")]
    public int ClienteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Informe um responsável válido.")]
    [Display(Name = "ID do responsável")]
    public int? ResponsavelId { get; set; }

    [Required(ErrorMessage = "Informe o equipamento.")]
    [StringLength(150)]
    public string Equipamento { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o modelo.")]
    [StringLength(150)]
    public string Modelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descreva o problema apresentado.")]
    [StringLength(2000)]
    [Display(Name = "Descrição do problema")]
    public string DescricaoProblema { get; set; } = string.Empty;

    [StringLength(2000)]
    [Display(Name = "Diagnóstico técnico")]
    public string? DiagnosticoTecnico { get; set; }

    [StringLength(2000)]
    [Display(Name = "Solução aplicada")]
    public string? SolucaoAplicada { get; set; }

    [Required(ErrorMessage = "Informe a data de abertura.")]
    [Display(Name = "Data de abertura")]
    public DateTime DataAbertura { get; set; } = DateTime.Now;

    [Display(Name = "Data de previsão")]
    public DateTime? DataPrevisao { get; set; }

    [Display(Name = "Data de conclusão")]
    public DateTime? DataConclusao { get; set; }

    [Required(ErrorMessage = "Selecione o status.")]
    [StringLength(30)]
    public string Status { get; set; } = "Aberta";

    [Required(ErrorMessage = "Selecione a prioridade.")]
    [StringLength(20)]
    public string Prioridade { get; set; } = "Normal";

    [StringLength(2000)]
    [Display(Name = "Observações")]
    public string? Observacoes { get; set; }

    [Range(typeof(decimal), "0", "9999999999999999", ErrorMessage = "O valor do serviço não pode ser negativo.")]
    [Display(Name = "Valor do serviço")]
    public decimal ValorServico { get; set; } = 0m;

    [Range(typeof(decimal), "0", "9999999999999999", ErrorMessage = "O valor de peças não pode ser negativo.")]
    [Display(Name = "Valor de peças")]
    public decimal ValorPecas { get; set; } = 0m;
}
