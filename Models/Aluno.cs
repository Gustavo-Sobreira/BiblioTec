using System.ComponentModel.DataAnnotations;

namespace BackBiblioteca.Models;

public class Aluno
{
    [Key]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int matricula { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string nome { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int sala { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string turno { get; set; }
}