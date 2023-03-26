
using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface ILivroService
{
    string Apagar(int registro);
    Livro? BuscarPorRegistro(int registro);
    string Cadastrar(Livro livroParaAdicionar);
    string Editar(Livro livroParaEditar);
    bool VerificarRegistro(int registro);
    string CompararCampos(Livro livroEmVerificacao);
    string FormatarTextos(string campoEmVerificacao);
    List<string>? ListarEstoque();
    string VerificarCampos(Livro livroEmVerificacao);
    bool VerificarPendenciaLivro(int registro);
    
    
}