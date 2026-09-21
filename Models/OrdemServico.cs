using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models
{
    public class OrdemServico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Aberta";

        [MaxLength(1000)]
        public string? Solicitacao { get; set; }

        [MaxLength(1000)]
        public string? Diagnostico { get; set; }

        [MaxLength(1000)]
        public string? Observacoes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;

        public DateTime? DataConclusao { get; set; }

        public bool Ativo { get; set; } = true;

        // Relacionamentos

        [ForeignKey(nameof(EmpresaId))]
        public Empresa Empresa { get; set; } = null!;

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; } = null!;

        public ICollection<OrdemServicoItem> Itens { get; set; }
            = new List<OrdemServicoItem>();
    }
}