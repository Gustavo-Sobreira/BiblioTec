using BackBiblioteca.Models;

namespace BackBiblioteca.Interface;

public interface IAlunoDao
{
    void Cadastrar(Aluno alunoParaAdicionar);
    Aluno? BuscarPorMatricula(string matricula);
    void Editar(Aluno alunoParaEditar);
    void Apagar(Aluno aluno);
}