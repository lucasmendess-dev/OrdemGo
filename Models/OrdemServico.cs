using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models;

[Table("OrdensServico", Schema = "dbo")]
public class OrdemServico
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(30)]
    [Column(TypeName = "varchar(30)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string NumeroOS { get; private set; } = string.Empty;

    public int ClienteId { get; set; }

    public int? ResponsavelId { get; set; }

    [Required, MaxLength(150)]
    public string Equipamento { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Modelo { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string DescricaoProblema { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? DiagnosticoTecnico { get; set; }

    [MaxLength(2000)]
    public string? SolucaoAplicada { get; set; }

    public DateTime DataAbertura { get; set; }

    public DateTime? DataPrevisao { get; set; }

    public DateTime? DataConclusao { get; set; }

    [Required, MaxLength(30)]
    [Column(TypeName = "varchar(30)")]
    public string Status { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string Prioridade { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Observacoes { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorServico { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorPecas { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Cliente Cliente { get; set; } = null!;

    public ICollection<OrdemServicoServico> Servicos { get; set; } = [];
}
