const URL_LIVRO = `https://localhost:7143/livro`;


function buscarLivros() {
    valorCampoPesquisa = document.getElementById("pg-livros-listar-campo-busca").value;
    
    var numero = parseInt(valorCampoPesquisa);
    
    if (!isNaN(numero)) {
        buscarLivrosPeloRegistro(valorCampoPesquisa);
    } else if(valorCampoPesquisa.trim() === ''){
      buscarTodos();
    }else{
      buscarLivrosPeloTitulo(valorCampoPesquisa);
    }
    
    document.getElementById("pg-livros-listar-campo-busca").value = "";
}

async function buscarTodos(){
  try {
    const response = await fetch(URL_LIVRO + "/buscar/todos/0/1000", {
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
    ul.innerHTML = "";
  
    criarLiParaTabelasLivro(livro, 0, ul);
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
    ul.innerHTML = "";
  
    listaDeLivros.forEach(function (livro, indice) {
      criarLiParaTabelasLivro(livro, indice, ul);
    });
  }


  function criarLiParaTabelasLivro(livro, indice, ul) {
    var novoLi = document.createElement("li");
    novoLi.classList.add("area-lista-tabela-dados");
  
    adicionarDadosTabelaLivro(livro, novoLi);
    adicionarBotoesTabelaLivro(novoLi, indice, livro);
  
    ul.appendChild(novoLi);
  }
  
  function adicionarDadosTabelaLivro(livro, novoLi) {
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
  }

  function adicionarBotoesTabelaLivro(novoLi, indice, livro) {
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
  
    botaoEditar.addEventListener("click", function () {
      abrirTelaEdicaoLivro(livro);
    });
  
    botaoApagar.addEventListener("click", function () {
      abrirTelaDeletar(indice, livro);
    });
  }