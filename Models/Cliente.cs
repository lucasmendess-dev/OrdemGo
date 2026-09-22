using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdemGo.Models;

[Table("Clientes", Schema = "dbo")]
public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(14)]
    [Column(TypeName = "varchar(14)")]
    public string CPF { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string? Telefone { get; set; }

    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(250)]
    public string? Endereco { get; set; }

    [MaxLength(100)]
    public string? Cidade { get; set; }

    [MaxLength(2)]
    [Column(TypeName = "char(2)")]
    public string? Estado { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "varchar(10)")]
    public string Status { get; set; } = string.Empty;
}
