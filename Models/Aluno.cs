using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTec.Models;

public class Aluno
{
    
    [Key]
    [Column(Order = 1)]
    [Required(ErrorMessage = "A matrícula do aluno é obrigatória",AllowEmptyStrings = false)]
    public string Matricula { get; set; }
    
    [Required(ErrorMessage = "O nome do aluno é um campo obrigatório")]
    public string NomeAluno { get; set; }
    
    public int NumeroDaSala { get; set; }
    
    public string Turno { get; set; }
    
    [ForeignKey("Codigo")]
    public string Codigo { get; set; }
}