using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [MaxLength(50)]
        public string? Codigo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorVenda { get; set; }

        public decimal Estoque { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(EmpresaId))]
        public Empresa Empresa { get; set; } = null!;
    }
}