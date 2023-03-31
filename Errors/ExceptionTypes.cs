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
}
