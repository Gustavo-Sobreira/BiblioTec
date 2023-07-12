namespace BackBiblioteca.Errors.Aluno;
public class PendenciaErros
{
    public class LivroTituloNaoEncontradoException : Exception
    {
        public class AlunoPendenteException : Exception
        {
            public AlunoPendenteException() : base("[ ERRO ] - Este aluno possui pendências com a biblioteca, por isso não é possível altera-lo.")
            {
            }
        }
        public class AlunoNaoPendenteException : Exception
        {
            public AlunoNaoPendenteException() : base("[ ERRO ] - Este aluno não possui pendências.")
            {
            }
        }
    }
}