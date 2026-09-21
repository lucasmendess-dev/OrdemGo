using System.ComponentModel.DataAnnotations;

namespace OrdemGo.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string NomeFantasia { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? RazaoSocial { get; set; }

        [MaxLength(18)]
        public string? Cnpj { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public ICollection<Usuario> Usuarios { get; set; }
            = new List<Usuario>();

        public ICollection<Cliente> Clientes { get; set; }
            = new List<Cliente>();

        public ICollection<Servico> Servicos { get; set; }
            = new List<Servico>();

        public ICollection<Produto> Produtos { get; set; }
            = new List<Produto>();

        public ICollection<OrdemServico> OrdensServico { get; set; }
            = new List<OrdemServico>();
    }
}