using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

[Table("t_livro")]
public class Livro
{
    [Key]
    [Column("id_registro")]
    [Required(ErrorMessage = "[ ERRO ] - Registro não pode ser nulo.")]
    public string? Registro { get; set; }
    
    [Column("nm_titulo")]
    [Required(ErrorMessage = "[ ERRO ] - Título não pode ser nulo.")]
    public string? Titulo { get; set; }
    
    [Column("nm_autor")]
    [Required(ErrorMessage = "[ ERRO ] - Autor não pode ser nulo.")]
    public string? Autor { get; set; }

    [Column("nm_editora")]
    [Required(ErrorMessage = "[ ERRO ] - A editora não pode ser nula.")]
    public string? Editora{ get; set; }

    [Column("fl_genero")]
    [Required(ErrorMessage = "[ ERRO ] - O gênnero do livro é obrigatório.")]
    public string? Genero { get; set; }
}