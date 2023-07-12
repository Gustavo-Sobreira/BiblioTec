
using BackBiblioteca.Models;

namespace BackBiblioteca.Interface;

public interface ILivroDao
{
    void Apagar(Livro livro);
    Livro? BuscarPorRegistro(string registro);
    void Cadastrar(Livro livroParaAdicionar);
    void Editar(Livro livroParaEditar);
    public List<Livro> ListarTodosLivrosExistentes(int skip, int take);
}