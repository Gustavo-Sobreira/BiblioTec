using Estoques.Data;
using Estoques.Data.Dtos;
using Estoques.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class EstoqueController : ControllerBase
{
    private EstoqueContext _context;

    public EstoqueController(EstoqueContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route(template: "lista_de_estoque")]
    public IEnumerable<ProdutoModel> ListarEstoque([FromQuery] int skip = 0, int take = 20)
    {
        return _context.Produto.Skip(skip).Take(take);
    }


    [HttpPost]
    [Route(template: "adicionar_produto")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public void AdicionarProdutoEstoque([FromBody] CreateProduto novo_produtoDto)
    {
        ProdutoModel novo_produto = new ProdutoModel
        {
            CodigoDeBarras = novo_produtoDto.CodigoDeBarras,
            //Decricao = novo_produtoDto.Decricao,
            Nome = novo_produtoDto.Nome,
            PrecoUnitario = novo_produtoDto.PrecoUnitario,
            Quantidade = novo_produtoDto.Quantidade
        };
        _context.Add(novo_produto);
        _context.SaveChanges();
    }


    [HttpPut]
    [Route(template: "atualizar_produto")]
    public IActionResult AtualizarProduto([FromBody] UpdateProduto produto_atualizadoDto)
    {
        var produtoEncontrado = _context.Produto.FirstOrDefault(
            produtoEncontrado => produtoEncontrado.CodigoDeBarras == produto_atualizadoDto.CodigoDeBarras
        );

        if (produtoEncontrado == null) return NotFound("Produto não encontrado, verifique o código de barras");

        produtoEncontrado.CodigoDeBarras = produto_atualizadoDto.CodigoDeBarras;
        //produtoEncontrado.Decricao = produto_atualizadoDto.Decricao;
        produtoEncontrado.Nome = produto_atualizadoDto.Nome;
        produtoEncontrado.PrecoUnitario = produto_atualizadoDto.PrecoUnitario;
        produtoEncontrado.Quantidade = produto_atualizadoDto.Quantidade;

        _context.SaveChanges();
        return Ok(produtoEncontrado);
    }

    [HttpDelete]
    [Route(template: "deletar_produto")]
    public IActionResult DeletarProduto([FromBody] long codigo_barras)
    {
        var produtoEncontrado = _context.Produto.FirstOrDefault(
            produtoEncontrado => produtoEncontrado.CodigoDeBarras == codigo_barras
        );

        if (produtoEncontrado == null) return NotFound("Produto não encontrado, verifique o código de barras");

        _context.Produto.Remove(produtoEncontrado);
        _context.SaveChanges();

        return Ok("Produto exluido com sucesso");
    }
}