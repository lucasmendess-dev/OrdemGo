using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models;

[Table("OrdensServicoServicos", Schema = "dbo")]
public class OrdemServicoServico
{
    public int OrdemServicoId { get; set; }

    public int ServicoId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }

    public OrdemServico OrdemServico { get; set; } = null!;

    public Servico Servico { get; set; } = null!;
}
