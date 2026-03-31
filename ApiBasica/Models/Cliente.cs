using System.ComponentModel.DataAnnotations;

namespace ApiBasica;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é obrigatória.")]
    [Range(18, int.MaxValue, ErrorMessage = "O cliente deve ter pelo menos 18 anos.")]
    public int Idade { get; set; }
}