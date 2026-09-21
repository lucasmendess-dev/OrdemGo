using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(18)]
        public string? CpfCnpj { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Endereco { get; set; }

        [MaxLength(100)]
        public string? Cidade { get; set; }

        [MaxLength(2)]
        public string? Estado { get; set; }

        [MaxLength(10)]
        public string? Cep { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(EmpresaId))]
        public Empresa Empresa { get; set; } = null!;

        public ICollection<OrdemServico> OrdensServico { get; set; }
            = new List<OrdemServico>();
    }
}