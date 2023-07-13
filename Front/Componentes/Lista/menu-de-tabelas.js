const NOME_TABELA_ALUNO = "t_aluno";
const NOME_TABELA_LIVRO = "t_livro";

function buscarElementosParaTabela(nomeTabela) {
  if (nomeTabela == NOME_TABELA_LIVRO) {
    buscarLivros();
  } else if (nomeTabela == NOME_TABELA_ALUNO) {
    buscarAlunos();
  }
}


