using System.ComponentModel.DataAnnotations;

namespace BackBiblioteca.Models;

public class Livro
{
    [Key]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int codigo { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string nome { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string autor { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int prazo { get; set; }
}