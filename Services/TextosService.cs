using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BackBiblioteca.Services;

public class TextosService
{
    

    public string FormatarTextos(string campoEmVerificacao)
    {
        //Remover Espaços
        campoEmVerificacao = campoEmVerificacao.Trim();
        campoEmVerificacao = Regex.Replace(campoEmVerificacao, @"\s+", " ");

        //Remove maiusculas
        campoEmVerificacao = campoEmVerificacao.ToLower();

        //Remove acentos
        campoEmVerificacao = Regex.Replace(
        campoEmVerificacao.Normalize(NormalizationForm.FormD),
        @"[\p{M}]+",
        "",
        RegexOptions.Compiled,
        TimeSpan.FromSeconds(0.5)
        );

        //Toda palavra começa com maiúscula
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        campoEmVerificacao = textInfo.ToTitleCase(campoEmVerificacao);

        return campoEmVerificacao;
    }

    public string FormatarIds(string campoEmVerificacao){
        //Remove traços indesejados
        campoEmVerificacao = Regex.Replace(campoEmVerificacao, "[.,-]","");
        campoEmVerificacao = Regex.Replace(campoEmVerificacao, @"\s+", "");

        campoEmVerificacao = FormatarTextos(campoEmVerificacao);

        return campoEmVerificacao;
    }
}
