using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackBiblioteca.Models;

[Table("t_livro")]
public class Livro
{
    [Key]
    [Column("id_registro")]
    [Required(ErrorMessage = "[ ERRO ] - Registro não pode ser nulo.")]
    [MaxLength(17,ErrorMessage = "[ ERRO ] - O registro de um livro pode conter apenas 15 caracteres.")]
    //Max de 17 pois pode ser passado da seguinte forma 978–00–000–0000–0, então para que não dê erro, isso é retirado na formatação 
    public string? Registro { get; set; }
    
    [Column("nm_titulo")]
    [Required(ErrorMessage = "[ ERRO ] - Título não pode ser nulo.")]
    [MaxLength(50,ErrorMessage = "[ ERRO ] - O titúlo do livro pode conter até 50 caracteres.")]

    public string? Titulo { get; set; }
    
    [Column("nm_autor")]
    [Required(ErrorMessage = "[ ERRO ] - Autor não pode ser nulo.")]
    [MaxLength(50,ErrorMessage = "[ ERRO ] - O nome do autor do livro pode conter até 50 caracteres.")]
    public string? Autor { get; set; }

    [Column("nm_editora")]
    [Required(ErrorMessage = "[ ERRO ] - A editora não pode ser nula.")]
    [MaxLength(20,ErrorMessage = "[ ERRO ] - O nome da editora do livro pode conter até 20 caracteres.")]
    public string? Editora{ get; set; }

    [Column("fl_genero")]
    [Required(ErrorMessage = "[ ERRO ] - O gênero do livro é obrigatório.")]
    [MaxLength(20,ErrorMessage = "[ ERRO ] - O gênero do livro pode conter até 20 caracteres.")]
    public string? Genero { get; set; }

    [Column("nm_prateleira")]
    [Required(ErrorMessage = "[ ERRO ] - A prateleira é obrigatória. ")]
    [MaxLength(10,ErrorMessage = "[ ERRO ] - A sigla da prateleira deve conter até 10 caracteres.")]
    public string? Prateleira { get; set; }
}