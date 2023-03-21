using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

[Table("aluno")]
public class Aluno
{
    [Key]
    [Column("matricula")]
    [Required(ErrorMessage = "[ERRO=001] Matrícula inválida.")]
    public int Matricula { get; set; }

    [Column("nome")]
    [Required(ErrorMessage = "[ERRO=002] Nome não pode ser nulo.")]
    public string? Nome { get; set; }

    [Column("sala")]
    [Required(ErrorMessage = "[ERRO=003] O n° da Sala não pode ser nulo.")]
    public ushort Sala { get; set; }

    [Column("turno")]
    [Required(ErrorMessage ="[ERRO=004] Os turnos manhã e tarde são representados por 1(manhã) e 2(tarde).")]
    public string Turno { get; set; }
}
