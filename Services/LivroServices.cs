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

    public string Cadastrar(Livro livroParaCadastro)
    {
        var livroTratdo = VerificarLivroValidoParaOperacao(livroParaCadastro);
        if (livroTratdo.Substring(0,8) != "[ERRO=008]")
        {
            if (livroTratdo == "")
            {
                var encontrado = PesquisarLivroPorRegistro(livroParaCadastro.Registro);
                return $"[ERRO=010] Registro já cadastrado.\n" +
                       $"Obra : {encontrado?.Titulo}\n" +
                       $"De: {encontrado?.Autor}\n" +
                       $"Registro: {encontrado?.Registro}";
            }
            return livroTratdo;
        }
        
        try
        {
            _context.Livros.Add(livroParaCadastro);
            _context.SaveChanges();
            return "Livro cadastrado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public string Apagar(Livro livroParaExclusao)
    {
        var livroTratado = VerificarLivroValidoParaOperacao(livroParaExclusao);
        if (livroTratado != "")
        {
            return livroTratado;
        }

        var livroEmprestado = VerificarPendenciaLivro(livroParaExclusao.Registro);
        if (livroEmprestado != "")
        {
            return livroEmprestado;
        }
        
        var livroCadastrado = PesquisarLivroPorRegistro(livroParaExclusao.Registro) ?? new Livro();
        if (livroCadastrado.Titulo != livroParaExclusao.Titulo)
        {
            return $"O titulo que você deseja apagar({livroParaExclusao.Titulo} e o título referente "
                   + $"\n ao registro({livroCadastrado.Titulo}), não coincidem.";
        }

        if (livroCadastrado.Autor != livroParaExclusao.Autor)
        {
            return $"O autor que você deseja apagar({livroParaExclusao.Autor} e o autor referente "
                   + $"\n ao registro({livroCadastrado.Autor}), não coincidem.";
        }

        try
        {
            _context.Livros.Remove(livroCadastrado);
            _context.SaveChanges();
            return "Livro apagado com sucesso";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private string VerificarLivroValidoParaOperacao(Livro livroParaVerificacao)
    {
        livroParaVerificacao = FormatarCampos(livroParaVerificacao);

        var camposInvalido = VerificarCamposInvalidos(livroParaVerificacao);
        if (camposInvalido != "")
        {
            return camposInvalido;
        }
        
        var livroExiste = PesquisarLivroPorRegistro(livroParaVerificacao.Registro);
        if (livroExiste == null)
        {
            return "[ERRO=008] Registro não encontrado";
        }
        
        return "";
    }
    private Livro FormatarCampos(Livro livroParaFormatacao)
    {
        livroParaFormatacao.Autor = livroParaFormatacao.Autor?.Trim();
        livroParaFormatacao.Titulo = livroParaFormatacao.Titulo?.Trim();
        return livroParaFormatacao;
    }
    private string VerificarCamposInvalidos(Livro livroEmVerificacao)
    {
        if (livroEmVerificacao.Registro == 0) {
            return "[ERRO==005] Registro não pode ser nulo.";
        }
        if (livroEmVerificacao.Autor == null) {
            return "[ERRO==006] Autor não pode ser nulo.";
        }
        if (livroEmVerificacao.Titulo == null) {
            return "[ERRO==007] Título não pode ser nulo.";
        }
        return "";
    }
    protected internal Livro? PesquisarLivroPorRegistro(int registro)
    {
        return _context.Livros.Find(registro);
    }
    public string VerificarPendenciaLivro(int registro)
    {
        var livroPendente = _context.Emprestimos
            .FirstOrDefault(emprestado => emprestado.Registro == registro);
        
        if (livroPendente == null)
        {
            return "";
        }
        Aluno? alunoComPendencia = _context.Alunos.Find(livroPendente.Matricula);
        var turno = alunoComPendencia?.Turno == "1" ? "Manhã" : "Tarde";
        
        return "[ERRO=009] Este livro está emprestado para o aluno: \n" +
               $"{alunoComPendencia?.Nome}\n" +
               $"{alunoComPendencia?.Sala}\n" +
               $"{turno}\n" +
               $"Matrícula: {alunoComPendencia?.Matricula}";

    }
    
    //TODO Mudar para estoque real e não total
    public List<string> ListarEstoque()
    {
        var estoque = _context.Livros
            .GroupBy(livro => livro.Titulo, livro => livro.Autor)
            .Select(grupo => $"{grupo.Key}: {grupo.Count()}")
            .ToList();
        return estoque;
    }

    
}
