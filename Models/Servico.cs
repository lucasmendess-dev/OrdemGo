using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models;

[Table("Servicos", Schema = "dbo")]
public class Servico
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Descricao { get; set; } = string.Empty;

    [Column(TypeName = "decimal(8,2)")]
    public decimal TempoEstimadoHoras { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }
}
