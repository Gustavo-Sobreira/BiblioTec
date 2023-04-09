
using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface ILivroService
{
    public Livro? Cadastrar(Livro livroParaAdicionar);
    public void RegrasParaCadastrar(Livro livro);


    public Livro? Editar(Livro livroParaEditar);
    public void RegrasParaEditar(Livro livro);


    public Livro? Apagar(int registro);
    //public void CompararCampos(Livro livroEmVerificacao);


    public List<Livro> ListarEstoque();
    public Livro? BuscarPorRegistro(int registro);
    public bool VerificarRegistro(int registro);
    public void VerificarCampos(Livro livroEmVerificacao);
    public bool VerificarPendenciaLivro(int registro);
}
    
    
