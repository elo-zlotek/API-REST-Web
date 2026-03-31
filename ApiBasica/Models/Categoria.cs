using System.ComponentModel.DataAnnotations;

namespace ApiBasica;

public class Categoria
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 80 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "A descrição pode ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;
}