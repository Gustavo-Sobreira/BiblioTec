using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

[Table("livro")]
public class Livro
{
    [Key]
    [Column("registro")]
    [Required(ErrorMessage = "[ERRO==005] Registro não pode ser nulo.")]
    public int Registro { get; set; }
    
    [Column("titulo")]
    [Required(ErrorMessage = "[ERRO==006] Autor não pode ser nulo.")]
    public string? Titulo { get; set; }
    
    [Column("autor")]
    [Required(ErrorMessage = "[ERRO==007] Título não pode ser nulo.")]
    public string? Autor { get; set; }
}