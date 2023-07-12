namespace BackBiblioteca.Errors.Livro;
public class PendenteErros
{
    public class LivroPendenteException : Exception
    {
        public LivroPendenteException() : base("[ ERRO ] - Este livro está emprestado.")
        {
        }
    }

    public class LivroNaoPendenteException : Exception
    {
        public LivroNaoPendenteException() : base("[ ERRO ] - Este livro não foi emprestado.")
        {
        }
    }

    public class LivroComDevolucaoIncompativel : Exception
    {
        public LivroComDevolucaoIncompativel() : base("[ ERRO ] - Este livro não foi eemprestado a esse aluno.")
        {

        }
    }

}
