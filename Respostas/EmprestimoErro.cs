using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackBiblioteca.Respostas;

public class EmprestimoErro
{
    public static string Erro070 = "[ERRO=070] Este livro está emprestado para outro aluno.";
    public static string Erro071 = "[ERRO=071] Este aluno possui pendencias com a biblioteca.";
    public static string Erro072 = "[ERRO=072] Não há livro emprestado para este aluno.";
    public static string Erro073 = "[ERRO=073] O livro procurado não esta em estoque.";
    public static string Erro074 = "[ERRO=074] Este aluno está com livro pendente, porém este N° de Registro está vinculado a outro aluno.";

}
