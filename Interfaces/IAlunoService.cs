using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IAlunoService
{
    public Aluno? BuscarAlunoPorMatricula(int matricula);
    
    
    public string Cadastrar(Aluno alunoParaAdicionar);
    public void RegrasParaCadastro(Aluno aluno);


    public string Editar(Aluno alunoParaEditar);
    public void RegrasParaEdicao(Aluno aluno);


    public string Apagar(int matricula);
    public void CompararDadosDeAluno(Aluno alunoEmVerificacao);
    

    public string FormatarTextos(string campoEmVerificacao);

    
    public void VerificarCampos(Aluno alunoEmVerificacao);
    public bool VerificarMatriculaExiste(int matricula);
    public bool VerificarPendenciaAluno(int matricula);

}