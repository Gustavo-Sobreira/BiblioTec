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
        bool encontrado = PesquisarLivroPorRegistro(livroEmVerificacao.codigo);
        if (encontrado)
        {
            return "Este N° de Registro já está sendo utilizado";
        }
        _context.livro.Add(livroEmVerificacao);
        _context.SaveChanges();
        return "Livro cadastrado com sucesso";
    }
    
    public bool PesquisarLivroPorRegistro(int registro)
    {
        Livro? livroEncontrado = _context.livro
            .FirstOrDefault(livro => livro.codigo == registro);
        if (livroEncontrado == null)
        {
            return false;
        }
        return true;
    }
    
    
    
    public string EmprestarLivro(int registro, int matricula)
    {
        Emprestado dadosParaEmprestimo = new Emprestado()
        {
            id_emprestimo = 0,
            matricula_aluno = matricula,
            codigo_livro = registro
        };
        string validado = ValidarEmprestimo((dadosParaEmprestimo));
        if (validado == "")
        {
            var ultimoID = _context.emprestado
                .OrderBy(emprestado => emprestado.id_emprestimo)
                .Last().id_emprestimo;
            
            dadosParaEmprestimo.id_emprestimo = ultimoID + 1;
            
            _context.emprestado.Add(dadosParaEmprestimo);
            _context.SaveChanges();
            return $"Emprétimo realizado com sucesso. ID = {dadosParaEmprestimo.id_emprestimo}";
        }

        return validado;
    }

    private string ValidarEmprestimo(Emprestado dadosParaEmprestimo)
    {
        AlunoServices dadosDoAluno = new AlunoServices(_context);
        
        bool alunoExiste = dadosDoAluno.VerificarMatriculaAluno(dadosParaEmprestimo.matricula_aluno);
        if (alunoExiste)
        {
            bool pendenciaAluno = dadosDoAluno.VerificarPendenciaAluno(dadosParaEmprestimo.matricula_aluno);
            if (pendenciaAluno)
            {
                return "Este aluno possui pendencias. Para que o emprétimo seja liberado, deve ser feita a devolução";
            }
            
            bool livroExiste = PesquisarLivroPorRegistro(dadosParaEmprestimo.codigo_livro);
            if (livroExiste)
            {
                return "";
            }
            return "Este N° de registro não foi encontrado.";
        }
        return "Está matrícula não está registrada, favor registar o aluno.";
    }


    
    private int PesquisarEmprestimo(int matricula)
    {
        int? codigoLivro = _context.emprestado.FirstOrDefault(emprestado => emprestado.matricula_aluno == matricula)?.codigo_livro;
        if (codigoLivro != null)
        {
            return (int)codigoLivro;
        }
        return 0;
    }
    public string DevolverLivro(int registro, int matricula)
    {
        int pendencia = PesquisarEmprestimo(matricula);
        if (pendencia == registro)
        {
            int id =_context.emprestado.First(emprestado => emprestado.codigo_livro == registro).id_emprestimo;
            Emprestado livroParaDevolucao = new Emprestado()
            {
                id_emprestimo = id,
                matricula_aluno = matricula,
                codigo_livro = registro
            };
            
            _context.emprestado.Remove(livroParaDevolucao);
            _context.SaveChanges();
            return "Devolução concuída!";
        }

        return "Falha na devolução, este aluno não pegou este livro";
    }
    
    
    
    public List<int> ListarLivrosIguais(string nomeLivro, string nomeAutor)
    {
        List<int> todosCodigosDesteTitulo = _context.livro
            .Where(livro => livro.nome == nomeLivro)
            .Where(livro => livro.autor == nomeAutor)
            .Select(livro => livro.codigo).ToList();
        
        return todosCodigosDesteTitulo;
        
        //Consigo retornar a quantidade total dos livros e fazer uma lista no front
    }
    
    public List<int> ListarLivrosIguaisEmprestados(string nomeLivro, string nomeAutor)
    {
        List<int> todosCodigosDesteTitulo = ListarLivrosIguais(nomeLivro,nomeAutor);

        List<int> numeroLivrosEmprestados = new List<int>();

        for (int i = 0; i < todosCodigosDesteTitulo.Count; i++)
        {
            Emprestado? livroEmprestado = _context.emprestado
                .FirstOrDefault(emprestado => emprestado.codigo_livro == todosCodigosDesteTitulo[i]);

            if (livroEmprestado != null)
            {
                numeroLivrosEmprestados.Add(livroEmprestado.codigo_livro);
            }
        }
        return numeroLivrosEmprestados;
    } 
    
    
}