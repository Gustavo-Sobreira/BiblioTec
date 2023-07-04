using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

[Table("aluno")]
public class Aluno
{
    [Key]
    [Column("id_matricula")]
    [Required(ErrorMessage = "[ ERRO ] - Matrícula não pode ser nula.")]
    [MaxLength(15, ErrorMessage = "[ ERRO ] - A matrícula não pode conter mais que 15(quinze) caracteres.")]
    public string? Matricula { get; set; }

    [Column("nm_aluno")]
    [Required(ErrorMessage = "[ ERRO ] - Nome do aluno não pode ser nulo.")]
    [MaxLength(50, ErrorMessage = "[ ERRO ] - O campo nome do aluno pode conter até 50 caracteres.")]
    public string? Nome { get; set; }

    [Column("nm_professor")]
    [Required(ErrorMessage = "[ ERRO ] - O nome do professor não pode ser nullo.")]
    [MaxLength(50, ErrorMessage = "[ ERRO ] - O campo nome do professor pode conter até 50 caracteres.")]
    public string? Professor {get; set;}

    [Column("vl_sala")]
    [Required(ErrorMessage = "[ ERRO ] - O n° da Sala não pode ser nulo.")]
    [MaxLength(3, ErrorMessage = "[ ERRO ] - O campo sala pode conter até 3 caracteres. (valor máximo 999)")]
    public string? Sala { get; set; }

    [Column("vl_turno")]
    [Required(ErrorMessage ="[ ERRO ] - O turno não pode ser nulo.")]
    [MaxLength(1, ErrorMessage = "[ ERRO ] - O campo turno pode conter somente 1(um) caracter. (sendo 1-manhã e 2-tarde)")]
    public string? Turno { get; set; }

    [Column("vl_serie")]
    [Required(ErrorMessage ="[ ERRO ] - A série não pode ser nula")]
    [MaxLength(1, ErrorMessage = "[ ERRO ] - O campo série pode conter somente 1 caracter. (valor máximo 9)")]
    public string? Serie { get; set; }
}
