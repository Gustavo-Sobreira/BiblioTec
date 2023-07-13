function abrirTelaEdicaoLivro(livro) {
    var pgEdicao = document.getElementById("pg-edicao");
    var campoAntigo = document.getElementById("pg-edicao-area-dados-antigos");
    var campoNovo = document.getElementById("pg-edicao-area-dados-novos");
  
    pgEdicao.classList.remove("hidden");
  
    preencherCamposDoEstadoAtualDoObjeto(campoAntigo, livro);
    preencherCamposDoEstadoFuturoDoObjeto(campoNovo, livro);
  }
  
  function preencherCamposDoEstadoAtualDoObjeto(campoAntigo, livro) {
    while (campoAntigo.firstChild) {
          campoAntigo.removeChild(campoAntigo.firstChild);
      }
      
      
      var pRegistro = document.createElement("p");
      pRegistro.textContent = livro.registro;
      campoAntigo.appendChild(pRegistro);
  
    var pTitulo = document.createElement("p");
    pTitulo.textContent = livro.titulo;
    campoAntigo.appendChild(pTitulo);
  
    var pAutor = document.createElement("p");
    pAutor.textContent = livro.autor;
    campoAntigo.appendChild(pAutor);
  
    var pEditora = document.createElement("p");
    pEditora.textContent = livro.editora;
    campoAntigo.appendChild(pEditora);
    
    var pGenero = document.createElement("p");
    pGenero.textContent = livro.genero;
    campoAntigo.appendChild(pGenero);
  
    var pPrateleira = document.createElement("p");
    pPrateleira.textContent = livro.prateleira;
    campoAntigo.appendChild(pPrateleira);
  }
  
  function preencherCamposDoEstadoFuturoDoObjeto(campoNovo, livro) {
    while (campoNovo.firstChild) {
      campoNovo.removeChild(campoNovo.firstChild);
    }
  
    var pRegistro2 = document.createElement("input");
    pRegistro2.name = "Registro";
    pRegistro2.value = livro.registro;
    campoNovo.appendChild(pRegistro2);
  
    var pTitulo2 = document.createElement("input");
    pTitulo2.name = "Titulo";
    pTitulo2.value = livro.titulo;
    campoNovo.appendChild(pTitulo2);
  
    var pAutor2 = document.createElement("input");
    pAutor2.name = "Autor";
    pAutor2.value = livro.autor;
    campoNovo.appendChild(pAutor2);
  
    var pEditora2 = document.createElement("input");
    pEditora2.name = "Editora";
    pEditora2.value = livro.editora;
    campoNovo.appendChild(pEditora2);
  
    var pGenero2 = document.createElement("input");
    pGenero2.name = "Genero";
    pGenero2.value = livro.genero;
    campoNovo.appendChild(pGenero2);
  
    var pPrateleira2 = document.createElement("input");
    pPrateleira2.name = "Prateleira";
    pPrateleira2.value = livro.prateleira;
    campoNovo.appendChild(pPrateleira2);
  }
  
  function pgEdicaoAreaBotoesCancelar() {
    var pgEdicao = document.getElementById("pg-edicao");
    pgEdicao.classList.add("hidden");
  }
  