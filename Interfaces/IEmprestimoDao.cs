using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IEmprestimoDao
{
    void Apagar(string registro);
    void Cadastrar(Emprestimo emprestimo);
    Emprestimo? BuscarPorMatricula(string matricula);
    Emprestimo? BuscarPorRegistro(string registro);
    void Editar(string matricula, string registro);
}