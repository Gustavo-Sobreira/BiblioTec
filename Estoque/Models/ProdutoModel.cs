using System.ComponentModel.DataAnnotations;

namespace Estoques.Models;

public class ProdutoModel
{
    [Key]
    [Required]
    public int Id { get; private set; }


    [Required]
    public long CodigoDeBarras { get; set; }


    [Required]
    public string? Nome { get; set; }


    [Required]
    public int Quantidade { get; set; }


    [Required]
    public double PrecoUnitario { get; set; }

    //public string? Decricao { get; set; }

}