const URL_ALUNO = `https://localhost:7143/aluno`;

function abrirPaginaAlunoCadastrar() {
  trocarPaginaAluno("pg-alunos-cadastrar");
}

function abrirPaginaAlunoListar() {
  trocarPaginaAluno("pg-alunos-listar");
}

function trocarPaginaAluno(paginaID){
  fecharPaginasAluno();

  const paginaParaAbrir = document.getElementById(paginaID);
  paginaParaAbrir.classList.remove("hidden");

}

function fecharPaginasAluno(){
  var listaDePaginas = document.getElementsByClassName("paginaAluno");
  for(i = 0; i < listaDePaginas.length; i++){
      listaDePaginas[i].classList.add("hidden")
  }
}
