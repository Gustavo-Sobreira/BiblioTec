namespace BackBiblioteca.Errors.Aluno;
public class DadosIncorretosErros
{
    public class AlunoDadosCorrompidosException : Exception
    {
        public AlunoDadosCorrompidosException() : base("[ ERRO ] - Os campos, matrícula, série, turno e sala não pode conter caracteres que não estejam entre 0 e 9.")
        {
        }
    }
}
