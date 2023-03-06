using System.ComponentModel.DataAnnotations;

namespace BiblioTec.Models.Livro;

public class Livro
{
    
    [Key]
    [Required(ErrorMessage = "O Código do livro é obrigatório")]
    public string Codigo { get; set; }
    
    
    [Required(ErrorMessage = "O nome do livro é obrigatório")]
    public string Nome { get; set; }
    
}