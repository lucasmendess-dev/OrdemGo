using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models
{
    public class OrdemServicoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrdemServicoId { get; set; }

        public int? ProdutoId { get; set; }

        public int? ServicoId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantidade { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [ForeignKey(nameof(OrdemServicoId))]
        public OrdemServico OrdemServico { get; set; } = null!;

        [ForeignKey(nameof(ProdutoId))]
        public Produto? Produto { get; set; }

        [ForeignKey(nameof(ServicoId))]
        public Servico? Servico { get; set; }
    }
}