namespace BackBiblioteca.Errors.Aluno;
public class SerieErros
{
    public class LivroTituloNaoEncontradoException : Exception
    {
        public class AlunoSerieException : Exception
        {
            public AlunoSerieException() : base("[ ERRO ] - A série de um aluno deve estar entre 1 e 9.")
            {
            }
        }
    }
}