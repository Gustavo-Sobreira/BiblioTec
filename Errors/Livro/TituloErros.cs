
namespace BackBiblioteca.Errors.Livro;
public class TituloErros
{
    public class  LivroTituloNaoEncontradoException: Exception
    {
        public LivroTituloNaoEncontradoException() : base("[ ERRO ] - Não foi possível localizar o livro.")
        {
        }
    }

}
