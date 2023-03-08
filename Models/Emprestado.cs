using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

public class Emprestimo
{
    [Key]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int id_emprestimo { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int codigo_livro { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int matricula_aluno { get; set; }
}