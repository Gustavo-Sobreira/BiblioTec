namespace BackBiblioteca.Errors.Aluno;
public class MatriculaErros
{
    public class LivroTituloNaoEncontradoException : Exception
    {
        public class AlunoMatriculaExistenteException : Exception
        {
            public AlunoMatriculaExistenteException() : base("[ ERRO ] - Matrícula existente, por favor verifique o número de matrícula do aluno.")
            {
            }
        }
        public class AlunoMatriculaNaoEncontradaException : Exception
        {
            public AlunoMatriculaNaoEncontradaException() : base("[ ERRO ] - Matrícula não encontrada.")
            {
            }
        }
        public class AlunoMatriculaInvalidaException : Exception
        {
            public AlunoMatriculaInvalidaException() : base("[ ERRO ] - A matrícula deve ser maior que 1(um).")
            {
            }
        }
    }
}