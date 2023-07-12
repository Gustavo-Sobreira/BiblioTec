using BackBiblioteca.Models;

namespace BackBiblioteca.Interface;

public interface IAlunoService
{
    public Aluno? BuscarAlunoPorMatricula(string matricula);
    
    
    public void Cadastrar(Aluno alunoParaAdicionar);
    public void RegrasParaCadastro(Aluno aluno);
    


    public Aluno? Editar(Aluno alunoParaEditar);
    public void RegrasParaEdicao(Aluno aluno);


    public Aluno? Apagar(string matricula);
    //public void CompararDadosDeAluno(Aluno alunoEmVerificacao);
    
    
    public void VerificarCampos(Aluno alunoEmVerificacao);
    public bool VerificarPendenciaAluno(string matricula);

}