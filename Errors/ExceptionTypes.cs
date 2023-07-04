using BackBiblioteca.Respostas;

namespace BackBiblioteca.Errors
{

    //MATRÍCULA
    public class AlunoMatriculaExistenteException : Exception
    {
        public AlunoMatriculaExistenteException() : base("[ ERRO ] - A matrícula existente, por favor verifique o número de matrícula do aluno.")
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
        public AlunoMatriculaInvalidaException() : base("[ ERRO ] - A matrícula deve ser maior que 1(um).")
        {
        }
    }

    //NOME ALUNO
    public class AlunoNomeIncompativelException : Exception
    {
        public AlunoNomeIncompativelException() : base(ErrorMensage.AlunoNomeIncompativel)
        {
        }
    }
    public class AlunoNomeInvalidoException : Exception
    {
        public AlunoNomeInvalidoException() : base("[ ERRO ] - Nome do aluno é inválido. O nome deve haver até 50 caracteres.")
        {
        }
    }
    public class AlunoProfessorInválidoException : Exception
    {
        public AlunoProfessorInválidoException() : base("[ ERRO ] - Nome do professor é inválido. O nome deve haver até 50 caracteres.") 
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

//SÉRIE
    public class AlunoSerieException : Exception
    {
        public AlunoSerieException() : base("[ ERRO ] - A série de um aluno deve estar entre 1 e 9.")
        {
        }
    }

//TURNO
    
    public class AlunoTurnoIncompativelException : Exception
    {
        public AlunoTurnoIncompativelException() : base(ErrorMensage.AlunoTurnoIncompativel)
        {
        }
    }
    public class AlunoTurnoIncorretoException : Exception
    {
        public AlunoTurnoIncorretoException():base("[ ERRO ] - O turno deve conter valores entre 1(manhã) e 2(tarde)")
        {
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
