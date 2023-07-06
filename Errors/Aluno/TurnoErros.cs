namespace BackBiblioteca.Errors.Aluno;
public class TurnoErros
{
    public class LivroTituloNaoEncontradoException : Exception
    {
        public class AlunoTurnoIncorretoException : Exception
        {
            public AlunoTurnoIncorretoException() : base("[ ERRO ] - O turno deve conter valores entre 1(manhã) e 2(tarde)")
            {
            }
        }
    }
}