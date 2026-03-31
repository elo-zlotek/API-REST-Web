using System.ComponentModel.DataAnnotations;

namespace ApiBasica;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(typeof(decimal), "0", "99999999999,99", ErrorMessage = "O preço não pode ser negativo")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O estoque é obrigatório.")]
    [Range(0, int.MaxValue, ErrorMessage = "O estoque deve ser maior ou igual a 0.")]
    public int Estoque { get; set; }
}