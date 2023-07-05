
using BackBiblioteca.Models;
using BackBiblioteca.Services.DTO;

namespace BackBiblioteca.stringerfaces;

public interface ILivroService
{
    public Livro? Cadastrar(Livro livroParaAdicionar);
    public void RegrasParaCadastrar(Livro livro);


    public Livro? Editar(Livro livroParaEditar);
    public void RegrasParaEditar(Livro livro);


    public Livro? Apagar(string registro);
    //public void CompararCampos(Livro livroEmVerificacao);


    public IEnumerable<LivroDTO> ListarEstoqueDisponivelParaEmprestimo(int skip, int take);
    public Livro? BuscarPorRegistro(string registro);
    public void VerificarCampos(Livro livroEmVerificacao);
    public bool VerificarPendenciaLivro(string registro);
}