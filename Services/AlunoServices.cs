using BackBiblioteca.Data;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services;

public class AlunoServices
{
    private readonly BibliotecContext _context;

    public AlunoServices(BibliotecContext context)
    {
        _context = context;
    }

    public string CadastrarAluno(Aluno novoAluno)
    {
        var matriculaExiste = VerificarMatriculaAluno(novoAluno.matricula);
        try
        {
            if (matriculaExiste != "")
            {
                return matriculaExiste;
            }
            _context.aluno.Add(novoAluno);
            _context.SaveChanges();
            return "Aluno cadastrado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string VerificarMatriculaAluno(int matriculaAluno)
    {
        if (matriculaAluno <= 0)
        {
            return "Campo de matrícula inválido";
        }

        var matriculaExistente = _context.aluno.FirstOrDefault(aluno => aluno.matricula == matriculaAluno);
        if (matriculaExistente != null)
        {
            return "Está matrícula já existe";
        }
        return "";
    }

    public Emprestado? VerificarPendenciaAluno(int matriculaAluno)
    {
        var pendencia = _context.emprestado.FirstOrDefault(emprestado => emprestado.matricula_aluno == matriculaAluno);
        return pendencia;
    }

    public string ApagarAluno(Aluno dadosAlunoParaApagar)
    {
        var matriculaValida = VerificarMatriculaAluno(dadosAlunoParaApagar.matricula);
        if (matriculaValida == "")
        {
            return "Matrícula não encontrada.";
        }

        var pendencia = VerificarPendenciaAluno(dadosAlunoParaApagar.matricula);
        if (pendencia != null)
        {
            return $"Este aluno possui pendencias com a biblioteca. N° Livro: {pendencia.codigo_livro}";
        }

        var alunoCadastrado = _context.aluno.Find(dadosAlunoParaApagar.matricula);
        if (alunoCadastrado?.nome != dadosAlunoParaApagar.nome)
        {
            return "Os nomes dos alunos, são incompatíveis." +
                   $"\nAluno cadastrado: {alunoCadastrado?.nome}" +
                   $"\nAluno recebido: {dadosAlunoParaApagar.nome}";
        }

        try
        {
            _context.aluno.Remove(alunoCadastrado);
            _context.SaveChanges();
            return "Aluno removido com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}