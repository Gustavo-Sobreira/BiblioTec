using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IEmprestimoDao
{
    void Apagar(int registro);
    void Cadastrar(Emprestimo emprestimo);
    Emprestimo? BuscarPorMatricula(int matricula);
    Emprestimo? BuscarPorRegistro(int registro);
    void Editar(int matricula, int registro);
}