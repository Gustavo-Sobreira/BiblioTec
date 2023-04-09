
using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface ILivroDao
{
    void Apagar(Livro livro);
    Livro? BuscarPorRegistro(int registro);
    void Cadastrar(Livro livroParaAdicionar);
    void Editar(Livro livroParaEditar);
    public List<string> ListarEstoque();
}