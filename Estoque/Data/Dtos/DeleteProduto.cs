using System.ComponentModel.DataAnnotations;

namespace Estoques.Data.Dtos;

public class DeleteProduto
{
    [Required]
    [Range(100000000000,9999999999999, ErrorMessage = "O código de barras informado deve ter de 12 a 13 caracteres")]
    public long CodigoDeBarras { get; set; }


    [Required]
    public string? Nome { get; set; }
}
