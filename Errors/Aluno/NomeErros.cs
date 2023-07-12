namespace BackBiblioteca.Errors.Aluno;
public class NomeErros
{
    public class AlunoNomeException : Exception
    {
        public class AlunoNomeNaoEncontradoException : Exception
        {
            public AlunoNomeNaoEncontradoException() : base("[ ERRO ] - Não foi possível encontrar este aluno.")
            {
            }
        }
    }
}