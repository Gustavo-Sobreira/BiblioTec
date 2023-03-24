using BackBiblioteca.Data;
using BackBiblioteca.Respostas;
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
       
        if (livroTratdo == "")
        {
            var encontrado = PesquisarLivroPorRegistro(livroParaCadastro.Registro);
            return LivroErro.Erro042;
        }
        
        if (livroTratdo != LivroErro.Erro041)
        {
            return livroTratdo;
        }
        
        try
        {
            _context.Livros.Add(livroParaCadastro);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso001;
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
            return LivroErro.Erro061;
        }

        if (livroCadastrado.Autor != livroParaExclusao.Autor)
        {
            return LivroErro.Erro051;
        }

        try
        {
            _context.Livros.Remove(livroCadastrado);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso002;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public string Editar(Livro livroParaEdicao){

        livroParaEdicao = FormatarCampos(livroParaEdicao);

        var dadosValidos = VerificarLivroValidoParaOperacao(livroParaEdicao);
        if (dadosValidos != "")
        {
            return dadosValidos;
        }

        var livro = _context.Livros.Find(livroParaEdicao.Registro);
        try
        {
            if (livro == null)
            {
                return "Erro";
            }
            _context.Livros.Remove(livro);
            _context.Livros.Add(livroParaEdicao);
            _context.SaveChanges();
            return OperacaoConcluida.Sucesso003;
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
            return LivroErro.Erro041;
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
            return LivroErro.Erro040;
        }
        if (livroEmVerificacao.Autor == null) {
            return LivroErro.Erro050;
        }
        if (livroEmVerificacao.Titulo == null) {
            return LivroErro.Erro060;
        }
        return "";
    }
    protected internal Livro? PesquisarLivroPorRegistro(int registro)
    {
        var livro = _context.Livros.Find(registro);
        _context.SaveChanges();
        return livro;
    }
    public string VerificarPendenciaLivro(int registro)
    {
        var livroPendente = _context.Emprestimos
            .FirstOrDefault(emprestado => emprestado.Registro == registro);
        
        if (livroPendente == null)
        {
            return "";
        }

        return EmprestimoErro.Erro070;
    }
    
    //TODO Mudar para estoque real e não total
    public List<string> ListarEstoque()
    {
        var estoque = _context.Livros
            .Where(livro => 
                !_context.Emprestimos.Any(emprestimo => emprestimo.Registro == livro.Registro))
            .GroupBy(livro => livro.Titulo, livro => livro.Autor)
            .OrderBy(grupo => grupo.Key)
            .Select(grupo => $"{grupo.Key}: {grupo.Count()}")
            .ToList();
        return estoque;
    }
    
}