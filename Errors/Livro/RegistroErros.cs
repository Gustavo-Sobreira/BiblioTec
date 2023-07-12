namespace BackBiblioteca.Errors.Livro;
public class RegistroErros
{
    public class LivroRegistroExistenteException : Exception
    {
        public LivroRegistroExistenteException() : base("[ ERRO ] - Este registro já existe.")
        {
        }
    }
    public class LivroRegistroNuloException : Exception
    {
        public LivroRegistroNuloException() : base("[ ERRO ] - O registro não é válido, todo registro deve ser maior que 0(zero).")
        {
        }
    }
    public class LivroRegistroNaoEncontradoException : Exception
    {
        public LivroRegistroNaoEncontradoException() : base("[ ERRO ] - Registro não encontrado.")
        {
        }
    }

}
