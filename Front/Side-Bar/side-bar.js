function sideBarGeral(){
    trocarPagina('pg-geral')
}
function sideBarEmprestimo(){
    trocarPagina('pg-emprestimo')
}
function sideBarLivros(){
    trocarPagina('pg-livros')
}
function sideBarAlunos(){
    trocarPagina('pg-alunos')
}
function sideBarPendentes(){
    trocarPagina('pg-pendentes')
}

function trocarPagina(paginaID){
    fecharPaginas();

    const paginaParaAbrir = document.getElementById(paginaID);
    paginaParaAbrir.classList.remove("hidden");

}

function fecharPaginas(){
    var listaDePaginas = document.getElementsByClassName("pagina");
    for(i = 0; i < listaDePaginas.length; i++){
        listaDePaginas[i].classList.add("hidden")
    }
}
