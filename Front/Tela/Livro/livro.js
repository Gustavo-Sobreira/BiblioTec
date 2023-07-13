function abrirPaginaLivroCadastrar() {
  trocarPaginaLivro("pg-livros-cadastrar");
}

function abrirPaginaLivroListar() {
  trocarPaginaLivro("pg-livros-listar");
}

function trocarPaginaLivro(paginaID){
  fecharPaginasLivro();

  const paginaParaAbrir = document.getElementById(paginaID);
  paginaParaAbrir.classList.remove("hidden");


}

function fecharPaginasLivro(){
  var listaDePaginas = document.getElementsByClassName("paginaLivro");
  for(i = 0; i < listaDePaginas.length; i++){
      listaDePaginas[i].classList.add("hidden")
      console.log(listaDePaginas[i].classList)
  }
}
