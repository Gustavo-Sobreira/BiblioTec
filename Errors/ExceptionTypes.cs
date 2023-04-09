using BackBiblioteca.Respostas;

namespace BackBiblioteca.Errors
{
    public class AlunoMatriculaExistenteException : Exception
    {
        public AlunoMatriculaExistenteException() : base(ErrorMensage.AlunoMatriculaExistente)
        {
        }
    }
    public class AlunoMatriculaNaoEncontradaException : Exception
    {
        public AlunoMatriculaNaoEncontradaException() : base(ErrorMensage.AlunoMatriculaNaoEncontrada)
        {
        }
    }
    public class AlunoMatriculaInvalidaException : Exception
    {
        public AlunoMatriculaInvalidaException() : base(ErrorMensage.AlunoMatriculaInvalida)
        {
        }
    }

    
    public class AlunoNomeIncompativelException : Exception
    {
        public AlunoNomeIncompativelException() : base(ErrorMensage.AlunoNomeIncompativel)
        {
        }
    }
    public class AlunoNomeInvalidoException : Exception
    {
        public AlunoNomeInvalidoException() : base(ErrorMensage.AlunoNomeInvalido)
        {
        }
    }
    
    
    public class AlunoPendenteException : Exception
    {
        public AlunoPendenteException() : base(ErrorMensage.AlunoPendente)
        {
        }
    }
    public class AlunoNaoPendenteException : Exception
    {
        public AlunoNaoPendenteException() : base(ErrorMensage.AlunoNaoPendente)
        {
        }
    }


    
    public class AlunoSalaNuloException : Exception
    {
        public AlunoSalaNuloException() : base(ErrorMensage.AlunoSalaNulo){
        }
    }
    public class AlunoSalaIncompativelException : Exception
    {
        public AlunoSalaIncompativelException() : base(ErrorMensage.AlunoSalaIncompativel)
        {
        }
    }

    
    public class AlunoTurnoIncompativelException : Exception
    {
        public AlunoTurnoIncompativelException() : base(ErrorMensage.AlunoTurnoIncompativel)
        {
        }
    }
    public class AlunoTurnoIncorretoException : Exception
    {
        public AlunoTurnoIncorretoException():base(ErrorMensage.AlunoTurnoIncorreto){
        }
    }
    
    
    public class LivroRegistroExistenteException : Exception
    {
        public LivroRegistroExistenteException():base(ErrorMensage.LivroRegistroExistente)
        {
        }
    }
    public class LivroTituloIncompativelException : Exception
    {
        public LivroTituloIncompativelException() : base(ErrorMensage.LivroTituloIncompativel)
        {
        }
    }
    
    public class LivroTituloNuloException : Exception
    {
        public LivroTituloNuloException() : base(ErrorMensage.LivroTituloNulo)
        {
        }
    }
    
    public class LivroAutorNuloException : Exception
    {
        public LivroAutorNuloException() : base(ErrorMensage.LivroAutorNulo)
        {
        }
    }
    
    public class LivroRegistroNuloException : Exception
    {
        public LivroRegistroNuloException() : base(ErrorMensage.LivroRegistroNulo)
        {
        }
    }
    
    public class LivroAutorIncompativelException : Exception
    {
        public LivroAutorIncompativelException() : base(ErrorMensage.LivroAutorIncompativel)
        {
        }
    }
    
    public class LivroPendenteException : Exception
    {
        public LivroPendenteException() : base(ErrorMensage.LivroPendente)
        {
        }
    }
    
    public class LivroNaoPendenteException : Exception
    {
        public LivroNaoPendenteException() : base(ErrorMensage.LivroNaoPendente)
        {
        }
    }
    
    public class LivroRegistroNaoEncontradoException : Exception
    {
        public LivroRegistroNaoEncontradoException() : base(ErrorMensage.LivroRegistroNaoEncontrado)
        {
        }
    }
}
