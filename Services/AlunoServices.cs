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
    
    public string Cadastrar(Aluno alunoParaAdicionar)
    {
        alunoParaAdicionar = FormatarCamposAluno(alunoParaAdicionar);
        
        var dadosValidos = VerificarDadosValidos(alunoParaAdicionar);
        if (dadosValidos != "")
        {
            return dadosValidos;
        }

        try
        {
            _context.Alunos.Add(alunoParaAdicionar);
            _context.SaveChanges();
            return "Aluno cadastrado com sucesso.";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public string Apagar(Aluno alunoParaRemover)
    {
        alunoParaRemover = FormatarCamposAluno(alunoParaRemover);
        
        var dadosValidos = VerificarDadosValidos(alunoParaRemover);
        if (dadosValidos != "")
        {
            return dadosValidos;
        }
        
        var dadosCriticos = CompararDadosDeAluno(alunoParaRemover);
        if (dadosCriticos != "")
        {
            return dadosCriticos;
        }
        
        try
        {
            var alunoremovido = _context.Alunos.Find(alunoParaRemover.Matricula);
            if (alunoremovido == null)
            {
                return "Erro";
            }
            _context.Alunos.Remove(alunoremovido);
            _context.SaveChanges();
            return "Aluno removido com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public string Editar(Aluno alunoParaEditar)
    {
        alunoParaEditar = FormatarCamposAluno(alunoParaEditar);
        
        var dadosValidos = VerificarDadosValidos(alunoParaEditar);
        if (dadosValidos != "")
        {
            return dadosValidos;
        }
        
        var aluno = _context.Alunos.Find(alunoParaEditar.Matricula);
        try
        {
            if (aluno == null)
            {
                return "Erro";
            }
            _context.Alunos.Remove(aluno);
            _context.Alunos.Add(alunoParaEditar);
            _context.SaveChanges();
            return "Aluno editado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string VerificarDadosValidos(Aluno alunoEmVerificacao)
    {
        var campoInvalido = VerificarCamposInvalidos(alunoEmVerificacao);
        if (campoInvalido != "")
        {
            return campoInvalido;
        }

        var matriculaValida = VerificarMatricula(alunoEmVerificacao.Matricula);
        if (!matriculaValida)
        {
            return "Matrícula não encontrada.";
        }
        
        var pendencia = VerificarPendenciaAluno(alunoEmVerificacao.Matricula);
        if (pendencia != null)
        {
            return $"Este aluno possui pendencias com a biblioteca. N° Livro: {pendencia.Registro}";
        }

        return "";
    }
    private string VerificarCamposInvalidos(Aluno alunoEmVerificacao)
    {
        if (alunoEmVerificacao.Matricula <= 0)
        {
            return "[ERRO=001] Matrícula inválida.";
        }

        if (alunoEmVerificacao.Nome == "")
        {
            return "[ERRO=002] Nome não pode ser nulo.";
        }

        if (alunoEmVerificacao.Sala <= 0)
        {
            return "[ERRO=003] O n° da Sala não pode ser nulo.";
        }

        if (alunoEmVerificacao.Turno != "1" || alunoEmVerificacao.Turno != "2")
        {
            return "[ERRO=004] Os turnos manhã e tarde são representados por 1(manhã) e 2(tarde).";
        }
        return "";
    }
    protected internal  bool VerificarMatricula(int matriculaAluno)
    {
        return _context.Alunos.Any(aluno => aluno.Matricula == matriculaAluno);
    }
    protected internal Emprestimo? VerificarPendenciaAluno(int matriculaAluno)
    {
        return _context.Emprestimos
            .FirstOrDefault(emprestimo => emprestimo.Matricula == matriculaAluno);
    }
    
    private string CompararDadosDeAluno(Aluno alunoEmVerificacao)
    {
        var alunoCadastrado =
            _context.Alunos
                .Find(alunoEmVerificacao.Matricula);
        
        if (alunoCadastrado != alunoEmVerificacao)
        {
            if (alunoCadastrado?.Nome != alunoEmVerificacao.Nome)
            {
                return "Nomes incopatíveis.";
            }

            if (alunoCadastrado?.Sala != alunoEmVerificacao.Sala)
            {
                return "Salas incompatíveis.";
            }

            if (alunoCadastrado.Turno != alunoEmVerificacao.Turno)
            {
                return "Turnos incompatíveis";
            }
        }
        return "";
    }
    private Aluno FormatarCamposAluno(Aluno alunoParaFormatacao)
    {
        alunoParaFormatacao.Nome = alunoParaFormatacao.Nome?.Trim();
        alunoParaFormatacao.Turno = alunoParaFormatacao.Turno.Trim();
        return alunoParaFormatacao;
    }
}