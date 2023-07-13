const URL_LIVRO = `https://localhost:7143/livro`;

function buscarLivros() {
  pesquisa = document.getElementById("pg-livros-listar-campo-busca").value;

  var numero = parseInt(pesquisa);

  if (!isNaN(numero)) {
    buscarLivrosPeloRegistro(numero);
  } else {
    buscarLivrosPeloTitulo(pesquisa);
  }

  document.getElementById("pg-livros-listar-campo-busca").value = '';
}

async function buscarLivrosPeloRegistro(registro) {
  try {
    const response = await fetch(URL_LIVRO + "/buscar/" + registro, {
      method: "GET",
    });
    const data = await response.json();

    if (data.value.toString().startsWith("[ ERRO ]")) {
      const p_erro = document.getElementById("pg-erros");
      const campo_erro = document.getElementById("pg-erros-campo-de-erros");

      p_erro.classList.remove("hidden");
      campo_erro.innerHTML = data.value;
    } else {
      adicionarLivro(data.value);
    }
  } catch (error) {
    handleNetworkError(error);
  }
}

function adicionarLivro(livro) {
  var ul = document.getElementById("pg-livros-listar-resultados");
  ul.innerHTML = '';

  criarLI(livro, 0, ul);
}

async function buscarLivrosPeloTitulo(titulo) {
  try {
    const response = await fetch(
      URL_LIVRO + "/localizar/" + titulo + "/0/1000",
      {
        method: "GET",
      }
    );
    const data = await response.json();

    if (data.value.toString().startsWith("[ ERRO ]")) {
      const p_erro = document.getElementById("pg-erros");
      const campo_erro = document.getElementById("pg-erros-campo-de-erros");

      p_erro.classList.remove("hidden");
      campo_erro.innerHTML = data.value;
    } else {
      adicionarListaDeLivros(data.value);
    }
  } catch (error) {
    handleNetworkError(error);
  }
}

function adicionarListaDeLivros(listaDeLivros) {
  var ul = document.getElementById("pg-livros-listar-resultados");
  ul.innerHTML = '';

  listaDeLivros.forEach(function (livro, indice) {
    criarLI(livro, indice, ul);
  });
}

function criarLI(livro, indice, ul) {
  var novoLi = document.createElement("li");
  novoLi.classList.add("area-lista-tabela-dados");

  var pRegistro = document.createElement("p");
  pRegistro.textContent = livro.registro;
  novoLi.appendChild(pRegistro);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var pTitulo = document.createElement("p");
  pTitulo.textContent = livro.titulo;
  novoLi.appendChild(pTitulo);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var pAutor = document.createElement("p");
  pAutor.textContent = livro.autor;
  novoLi.appendChild(pAutor);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var pEditora = document.createElement("p");
  pEditora.textContent = livro.editora;
  novoLi.appendChild(pEditora);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var pGenero = document.createElement("p");
  pGenero.textContent = livro.genero;
  novoLi.appendChild(pGenero);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var pPrateleira = document.createElement("p");
  pPrateleira.textContent = livro.prateleira;
  novoLi.appendChild(pPrateleira);

  var span1 = document.createElement("span");
  novoLi.appendChild(span1);

  var divBotoes = document.createElement("div");

  var botaoEditar = document.createElement("button");
  botaoEditar.id = "livros-bt-editar" + indice;
  botaoEditar.classList.add("area-lista-dados-bt-editar");
  botaoEditar.textContent = "Editar";
  divBotoes.appendChild(botaoEditar);

  var botaoApagar = document.createElement("button");
  botaoApagar.id = "livros-bt-deletar" + indice;
  botaoApagar.classList.add("area-lista-dados-bt-apagar");
  botaoApagar.textContent = "Apagar";
  divBotoes.appendChild(botaoApagar);

  novoLi.appendChild(divBotoes);

  ul.appendChild(novoLi);

  botaoEditar.addEventListener("click", function () {
    abrirTelaEdicao(indice, livro);
  });

  botaoApagar.addEventListener("click", function () {
    abrirTelaDeletar(indice, livro);
  });
}

//TODO Arrumar
function abrirTelaEdicao(indice, livro) {
  // console.log("Abrir tela de edição para o livro de índice:", indice);
  // console.log("Dados do livro:", livro);

  var pgEdicao = document.getElementById("pg-edicao");
  var campoAntigo = document.getElementById("pg-edicao-area-dados-antigos");

  pgEdicao.classList.remove("hidden");

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

function abrirTelaDeletar(indice, livro){
  console.log("Abrir tela de edição para o livro de índice:", indice);
  console.log("Dados do livro:", livro);
}