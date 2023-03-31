
using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface ILivroService
{
    public string Cadastrar(Livro livroParaAdicionar);
    public void RegrasParaCadastrar(Livro livro);


    public string Editar(Livro livroParaEditar);
    public void RegrasParaEditar(Livro livro);


    public string Apagar(int registro);
    public void CompararCampos(Livro livroEmVerificacao);


    public List<string> ListarEstoque();
    public Livro? BuscarPorRegistro(int registro);
    public bool VerificarRegistro(int registro);
    public void VerificarCampos(Livro livroEmVerificacao);
    public bool VerificarPendenciaLivro(int registro);
}
    
    
