using BackBiblioteca.Data;
using BackBiblioteca.Respostas;
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
        if (dadosValidos != AlunoErro.Erro001)
        {
            if (dadosValidos == "" || dadosValidos == EmprestimoErro.Erro071)
            {
                return AlunoErro.Erro002;
            }
            return dadosValidos;
        }

        try
        {
            _context.Alunos.Add(alunoParaAdicionar);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso001;
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
            return OperacaoConcluida.Sucesso002;
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
            return OperacaoConcluida.Sucesso003;
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
            return AlunoErro.Erro001;
        }

        var pendencia = VerificarPendenciaAluno(alunoEmVerificacao.Matricula);
        if (pendencia != null)
        {
            return EmprestimoErro.Erro071;
        }

        return "";
    }

    private string VerificarCamposInvalidos(Aluno alunoEmVerificacao)
    {
        if (alunoEmVerificacao.Matricula <= 0)
        {
            return AlunoErro.Erro000;
        }

        if (alunoEmVerificacao.Nome == "")
        {
            return AlunoErro.Erro020;
        }

        if (alunoEmVerificacao.Sala <= 0)
        {
            return AlunoErro.Erro002;
        }

        if (alunoEmVerificacao.Turno != "1" && alunoEmVerificacao.Turno != "2")
        {
            return AlunoErro.Erro030;
        }
        return "";
    }

    protected internal bool VerificarMatricula(int matricula)
    {
        return _context.Alunos.Any(aluno => aluno.Matricula == matricula);
    }

    protected internal Emprestimo? VerificarPendenciaAluno(int matricula)
    {
        return _context.Emprestimos.FirstOrDefault(
            emprestimo => emprestimo.Matricula == matricula
        );
    }

    private string CompararDadosDeAluno(Aluno alunoEmVerificacao)
    {
        var alunoCadastrado = _context.Alunos.Find(alunoEmVerificacao.Matricula);

        if (alunoCadastrado != alunoEmVerificacao)
        {
            if (alunoCadastrado?.Nome != alunoEmVerificacao.Nome)
            {
                return AlunoErro.Erro021;
            }

            if (alunoCadastrado?.Sala != alunoEmVerificacao.Sala)
            {
                return AlunoErro.Erro011;
            }

            if (alunoCadastrado.Turno != alunoEmVerificacao.Turno)
            {
                return AlunoErro.Erro031;
            }
        }
        return "";
    }

    private Aluno FormatarCamposAluno(Aluno alunoParaFormatacao)
    {
        alunoParaFormatacao.Nome = alunoParaFormatacao.Nome?.Trim();
        alunoParaFormatacao.Turno = alunoParaFormatacao.Turno?.Trim();
        return alunoParaFormatacao;
    }
}
