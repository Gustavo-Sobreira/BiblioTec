using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;
[Table("emprestimo")]
public class Emprestimo
{
    [Key]
    [Column("id_emprestimo")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int IdEmprestimo { get; set; }
    
    [Column("registro")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int Registro { get; set; }
    
    [Column("matricula")]
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int Matricula { get; set; }
    
    [Column("eprestado")]
    public DateTime DataEmprestimo { get; set; }
}