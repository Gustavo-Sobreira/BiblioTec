namespace BackBiblioteca.Respostas;

public class ErrorMensage
{
    //Matricula
    public static readonly string AlunoMatriculaInvalida = "[ERRO=000] Matrícula inválida.";
    public static readonly string AlunoMatriculaNaoEncontrada = "[ERRO=001] Matrícula não encontrada.";
    public static readonly string AlunoMatriculaExistente = "[ERRO=002] Matrícula existente.";
    
    //Nome Aluno
    public static readonly string AlunoNomeInvalido = "[ERRO=020] Nome não pode ter mais de 100 caracteres.";
    public static readonly string AlunoNomeIncompativel = "[ERRO=021] O Nome do aluno não é compatível com o Nome so aluno encotrado nesta matrícula.";
    
    //Sala
    public static readonly string AlunoSalaNulo = "[ERRO=010] O N° da Sala não pode ser nulo.";
    public static readonly string AlunoSalaIncompativel = "[ERRO=011] O N° da Sala não é compatível com o N° de Sala encotrado nesta matrícula.";
    
    //Turno
    public static readonly string AlunoTurnoIncorreto = "[ERRO=030] Os turnos manhã e tarde são representados por 1(manhã) e 2(tarde).";
    public static readonly string AlunoTurnoIncompativel = "[ERRO=031] O Turno não é compatível com o Turno encotrado nesta matrícula.";
    
    //Pendencia
    public static readonly string LivroPendente = "[ERRO=070] Este livro está emprestado para outro aluno.";
    public static readonly string AlunoPendente = "[ERRO=071] Este aluno possui pendencias com a biblioteca.";
    public static readonly string AlunoNaoPendente = "[ERRO=072] Não há livro emprestado para este aluno.";
    public static readonly string LivroNaoPendente = "[ERRO=073] Este livro não está emprestado.";
    public static readonly string AlunoComLivroErrado = "[ERRO=074] Este aluno está com livro pendente, porém este N° de Registro está vinculado a outro aluno.";
    
    //Estoque
    public static readonly string LivroForaDeEstoque = "[ERRO=075] O livro procurado não está em estoque.";
    
    
    
    //Registro
    public static readonly string LivroRegistroNulo = "[ERRO=040] Registro não pode ser nulo.";
    public static readonly string LivroRegistroNaoEncontrado = "[ERRO=041] Registro não encontrado.";
    public static readonly string LivroRegistroExistente = "[ERRO=042] Registro já cadastrado.";

    //Autorreadonly 
    public static readonly string LivroAutorNulo = "[ERRO=050] Autor não pode ser nulo.";
    public static readonly string LivroAutorIncompativel = "[ERRO=051] O autor da obra que deseja apagar, não coincide com o autor encontrado neste N° de Registro.";
 
  
    //Títuloreadonly 
    public static readonly string LivroTituloNulo = "[ERRO=060] Título não pode ser nulo.";
    public static readonly string LivroTituloIncompativel = "[ERRO=061] O título deseja apagar, não coincide com o título encontrado neste N° de Registro.";
}