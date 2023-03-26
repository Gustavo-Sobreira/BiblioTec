using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IAlunoService
{
    string Apagar(int matricula);
    string Cadastrar(Aluno alunoParaAdicionar);
    string CompararDadosDeAluno(Aluno alunoEmVerificacao);
    Aluno? BuscarPorMatricula(int matricula);
    string Editar(Aluno alunoParaEditar);
    string FormatarTextos(string campoEmVerificacao);
    string VerificarCampos(Aluno alunoEmVerificacao);
    bool VerificarMatricula(int matricula);
    bool VerificarPendenciaAluno(int matricula);
    
}