namespace BackBiblioteca.Errors;
public class LivroErrors
{
    public class  LivroTituloNaoEncontradoException: Exception
    {
        public LivroTituloNaoEncontradoException() : base("[ ERRO ] - Não foi possível localizar o livro.")
        {
        }
    }
}
