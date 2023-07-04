using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IEmprestimoDao
{
    void Apagar(int registro);
    void Cadastrar(Emprestimo emprestimo);
    Emprestimo? BuscarPorMatricula(string matricula);
    Emprestimo? BuscarPorRegistro(int registro);
    void Editar(string matricula, int registro);
}