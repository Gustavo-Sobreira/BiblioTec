using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackBiblioteca.Respostas;

public class LivroErro
{
    //Registro
    public static string Erro040 = "[ERRO=040] Registro não pode ser nulo.";
    public static string Erro041 = "[ERRO=041] Registro não encontrado.";
    public static string Erro042 = "[ERRO=042] Registro já cadastrado.";

    //Autor
    public static string Erro050 = "[ERRO=050] Autor não pode ser nulo.";
    public static string Erro051 = "[ERRO=051] O autor da obra que deseja apagar, não coincide com o autor encontrado neste N° de Registro.";

 
    //Título
    public static string Erro060 = "[ERRO=060] Título não pode ser nulo.";
    public static string Erro061 = "[ERRO=061] O título deseja apagar, não coincide com o título encontrado neste N° de Registro.";

}
