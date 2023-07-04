using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IAlunoDao
{
    void Cadastrar(Aluno alunoParaAdicionar);
    Aluno? BuscarPorMatricula(string matricula);
    void Editar(Aluno alunoParaEditar);
    void Apagar(Aluno aluno);
}