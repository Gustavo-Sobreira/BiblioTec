using BackBiblioteca.Data;
using BackBiblioteca.Models;

namespace BackBiblioteca.Services;
public class LivroServices
{
    private readonly BibliotecContext _context;
    public LivroServices(BibliotecContext context)
    {
        _context = context;
    }
    
    public string CadastrarLivro(Livro livroEmVerificacao)
    {
        Livro livroExistente = PesquisarLivroPorRegistro(livroEmVerificacao.codigo)?? new Livro();
        try
        {
            if (livroExistente.codigo != 0)
            {
                return $"Este N° de Registro já está sendo utilizado na obra {livroExistente.titulo} de {livroExistente.autor}";
            }
            _context.livro.Add(livroEmVerificacao);
            _context.SaveChanges();
            
            return "Livro cadastrado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    private string VerificarLivroValidoParaAlteracao(int numeroRegistroLivro)
    {
        if (numeroRegistroLivro <= 0)
        {
            return "N° de registro inesistente";
        }
        
        var livroParaApagar = PesquisarLivroPorRegistro(numeroRegistroLivro);
        if (livroParaApagar == null)
        {
            return "N° de Registro não encontrado";   
        }
        
        var pendenciaLivro = _context.emprestado.FirstOrDefault(emprestado => emprestado.codigo_livro == numeroRegistroLivro);
        if (pendenciaLivro != null)
        {
            return $"Este livro está emprestado para o aluno {pendenciaLivro.matricula_aluno}." +
                   $"\nApós a devolução, conclua a exclusão.";    
        }
        return "";
    }
    
    
    public string EmprestarLivro(int numeroRegistroLivro, int matriculaAluno)
    {
        var validado = ValidarEmprestimo(numeroRegistroLivro, matriculaAluno);
        if (validado != "")
        {
            return validado;
        }
        
        var ultimoId = _context.emprestado.Max(emprestado => emprestado.id_emprestimo);
        var novoEmprestimo = new Emprestado()
        {
            id_emprestimo = ultimoId + 1,
            matricula_aluno = matriculaAluno,
            codigo_livro = numeroRegistroLivro
        };
        try
        {
            _context.emprestado.Add(novoEmprestimo);
            _context.SaveChanges();
            return $"Emprétimo realizado com sucesso. ID = {novoEmprestimo.id_emprestimo}";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private string ValidarEmprestimo(int numeroRegistroLivro, int matriculaAluno)
    {
        var alunoEmVerificacao = new AlunoServices(_context);
        
        var aluno = alunoEmVerificacao.VerificarMatriculaAluno(matriculaAluno);
        if (aluno == null)
        {
            return "Esta matrícula não está registrada, favor registrar o aluno.";
        }
        
        var pendenciaAluno = alunoEmVerificacao.VerificarPendenciaAluno(matriculaAluno);
        if (pendenciaAluno != null)
        {
            return $"Este aluno possui pendências, referentes ao N° de Registro {pendenciaAluno.codigo_livro}." +
                   $"\nPara que o empréstimo seja liberado, deve ser feita a devolução.";
        }

        var livroPodeSerAlterado = VerificarLivroValidoParaAlteracao(numeroRegistroLivro);
        if (livroPodeSerAlterado != "")
        {
            return livroPodeSerAlterado;
        }
        
        return "";
    }

    
    public string ApagarLivro(Livro dadosLivroParaExclusao)
    {
        var validoParaAlteracao = VerificarLivroValidoParaAlteracao(dadosLivroParaExclusao.codigo);
        if (validoParaAlteracao != "")
        {
            return validoParaAlteracao;
        }
       
        var livroParaApagar = PesquisarLivroPorRegistro(dadosLivroParaExclusao.codigo)?? new Livro();
        if (livroParaApagar.titulo != dadosLivroParaExclusao.titulo)
        {
            return $"O titulo que você deseja apagar({dadosLivroParaExclusao.titulo} e o título referente " +
                   $"\n ao registro({livroParaApagar.titulo}), não coincidem.";
        }

        if (livroParaApagar.autor != dadosLivroParaExclusao.autor)
        {
            return $"O autor que você deseja apagar({dadosLivroParaExclusao.autor} e o autor referente " +
                   $"\n ao registro({livroParaApagar.autor}), não coincidem.";
        }

        try
        {
            _context.livro.Remove(livroParaApagar);
            _context.SaveChanges();
            return "Livro apagado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public string DevolverLivro(int numeroRegistroLivro, int matriculaAluno)
    {
        AlunoServices aluno = new AlunoServices(_context);

        if (numeroRegistroLivro <= 0 ||matriculaAluno <= 0)
        {
            return "Favor adicione campos válidos";
        }

        try
        {
            var pendencia = aluno.VerificarPendenciaAluno(matriculaAluno);
            if (pendencia == null)
            {
                return "Este aluno não possui pendências.";
            }
        
            if (pendencia.codigo_livro != numeroRegistroLivro)
            {   
                return $"Falha na devolução, este aluno não pegou este livro (N° Registro {numeroRegistroLivro}).\n" +
                       $"Sua matrícula está vinculada ao N° Registro {pendencia.codigo_livro}";
            }
            
            var livroParaDevolucao =_context.emprestado.First(emprestado => emprestado.codigo_livro == numeroRegistroLivro);
            _context.emprestado.Remove(livroParaDevolucao);
            _context.SaveChanges();
            return "Devolução concuída!";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    public List<string> ListarEstoque()
    {
        var estoque = _context.livro
            .GroupBy(livro => livro.titulo, livro => livro.autor)
            .Select(grupo => $"{grupo.Key}: {grupo.Count()}")
            .ToList();
        return estoque;
    }
    private Livro? PesquisarLivroPorRegistro(int registro)
    {
        Livro? livroEncontrado = _context.livro.Find(registro);
        return livroEncontrado;
    }
}
