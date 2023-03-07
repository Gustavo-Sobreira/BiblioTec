using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTec.Models.Livro;

public class Livro
{
    
    [Key]
    [Required(ErrorMessage = "O Código do livro é obrigatório",AllowEmptyStrings = false)]
    public int codigo { get; set; }
    
    [Required(ErrorMessage = "O nome do livro é obrigatório")]
    public string nome { get; set; }
    
    
    [Required(ErrorMessage = "O nome do autor é obrigatório")]
    public string autor { get; set; }
    
    
    [Required(ErrorMessage = "O tempo de prazo é obrigatório")]
    public int prazo { get; set; }
}