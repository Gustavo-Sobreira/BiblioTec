using BackBiblioteca.Models;

namespace BackBiblioteca.Interfaces;

public interface IAlunoService
{
    public Aluno? BuscarAlunoPorMatricula(string matricula);
    
    
    public void Cadastrar(Aluno alunoParaAdicionar);
    public void RegrasParaCadastro(Aluno aluno);
    


    public Aluno? Editar(Aluno alunoParaEditar);
    public void RegrasParaEdicao(Aluno aluno);


    public Aluno? Apagar(string matricula);
    //public void CompararDadosDeAluno(Aluno alunoEmVerificacao);
    

    public string FormatarTextos(string campoEmVerificacao);

    
    public void VerificarCampos(Aluno alunoEmVerificacao);
    public bool VerificarMatriculaExiste(string matricula);
    public bool VerificarPendenciaAluno(string matricula);

}