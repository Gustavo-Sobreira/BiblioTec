namespace BackBiblioteca.Respostas;

public class AlunoErro
{
    //Matrícula
    public static string Erro000 = "[ERRO=000] Matrícula inválida.";
    public static string Erro001 = "[ERRO=001] Matrícula não encontrada.";
    public static string Erro002 = "[ERRO=002] Matrícula já cadastrada.";

    //Sala
    public static string Erro010 = "[ERRO=010] O N° da Sala não pode ser nulo.";
    public static string Erro011 = "[ERRO=011] O N° da Sala não é compatível com o N° de Sala encotrado nesta matrícula.";

  

    //Nome
    public static string Erro020 = "[ERRO=020] Nome não pode ser nulo.";
    public static string Erro021 = "[ERRO=021] O Nome do aluno não é compatível com o Nome so aluno encotrado nesta matrícula.";


    //Turno
    public static string Erro030 = "[ERRO=030] Os turnos manhã e tarde são representados por 1(manhã) e 2(tarde).";
    public static string Erro031 = "[ERRO=031] O Turno não é compatível com o Turno encotrado nesta matrícula.";
        
}