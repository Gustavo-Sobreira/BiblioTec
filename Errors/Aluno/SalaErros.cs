namespace BackBiblioteca.Errors.Aluno;
public class SalaErros
{
    public class LivroTituloNaoEncontradoException : Exception
    {
       public class AlunoSalaNuloException : Exception
        {
            public AlunoSalaNuloException() : base("[ ERRO ] - A sala deve ser maior que 0.")
            {
            }
        }
    }
}
