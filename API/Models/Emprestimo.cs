using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;
[Table("t_emprestimo")]
public class Emprestimo
{
    [Key]
    [Column("id_emprestimo")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdEmprestimo { get; set; }
    
    [Column("cd_registro")]
    [Required(ErrorMessage = "[ ERRO ] - O registro do livro é obrigatório.")]
    public string? Registro { get; set; }
    
    [Column("cd_matricula")]
    [Required(ErrorMessage = "[ ERRO ] - A matrícula do aluno é obrigatória.")]
    public string? Matricula { get; set; }
    
    [Column("dt_emprestimo", TypeName = "date")]
    public DateTime DataEmprestimo { get; set; }
}