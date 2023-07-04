
using BackBiblioteca.Models;

namespace BackBiblioteca.stringerfaces;

public interface ILivroService
{
    public Livro? Cadastrar(Livro livroParaAdicionar);
    public void RegrasParaCadastrar(Livro livro);


    public Livro? Editar(Livro livroParaEditar);
    public void RegrasParaEditar(Livro livro);


    public Livro? Apagar(string registro);
    //public void CompararCampos(Livro livroEmVerificacao);


    public List<Livro> ListarEstoque();
    public Livro? BuscarPorRegistro(string registro);
    public bool VerificarRegistro(string registro);
    public void VerificarCampos(Livro livroEmVerificacao);
    public bool VerificarPendenciaLivro(string registro);
}