function buscarAlunos() {
    valorCampoPesquisa = document.getElementById("pg-alunos-listar-campo-busca").value;
    
    var numero = parseInt(valorCampoPesquisa);
    
    if (!isNaN(numero)) {
        buscarAlunosPelaMatricula(valorCampoPesquisa);
    } else if(valorCampoPesquisa.trim() === ''){
      buscarTodosAlunos();
    }else{
      buscarAlunosPeloNome(valorCampoPesquisa);
    }
    
    document.getElementById("pg-alunos-listar-campo-busca").value = "";
}

async function buscarTodosAlunos(){
  try {
    const response = await fetch(URL_ALUNO + "/buscar/todos/0/1000", {
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
    adicionarListaDeAlunos(data.value);
  }
} catch (error) {
  handleNetworkError(error);
}
}


async function buscarAlunosPelaMatricula(matricula) {
    try {
        const response = await fetch(URL_ALUNO + "/buscar/" + matricula, {
            method: "GET",
        });
        
        const data = await response.json();
        
        if (data.value.toString().startsWith("[ ERRO ]")) {
            const p_erro = document.getElementById("pg-erros");
            const campo_erro = document.getElementById("pg-erros-campo-de-erros");
            
            p_erro.classList.remove("hidden");
            campo_erro.innerHTML = data.value;
        } else {
            adicionarAluno(data.value);
      }
    } catch (error) {
      handleNetworkError(error);
    }
  }
  

  function adicionarAluno(aluno) {
    var ul = document.getElementById("pg-alunos-listar-resultados");
    ul.innerHTML = "";
  
    criarLiParaTabelasAluno(aluno, 0, ul);
  }

  
//TODO adicionar skip take API
  async function buscarAlunosPeloNome(nome) {
    try {
      const response = await fetch(
        URL_ALUNO + "/buscar/aluno/" + nome + "/0/1000",
        {
          method: "GET",
        }
        );
        const data = await response.json();
        console.log(data)
  
      if (data.value.toString().startsWith("[ ERRO ]")) {
        const p_erro = document.getElementById("pg-erros");
        const campo_erro = document.getElementById("pg-erros-campo-de-erros");
  
        p_erro.classList.remove("hidden");
        campo_erro.innerHTML = data.value;
      } else {
        adicionarListaDeAlunos(data.value);
      }
    } catch (error) {
      handleNetworkError(error);
    }
  }
  
  function adicionarListaDeAlunos(listaDeAlunos) {
    var ul = document.getElementById("pg-alunos-listar-resultados");
    ul.innerHTML = "";

    listaDeAlunos.forEach(function (aluno, indice) {
      criarLiParaTabelasAluno(aluno, indice, ul);
    });
  }


  function criarLiParaTabelasAluno(aluno, indice, ul) {
   
    var novoLi = document.createElement("li");
    novoLi.classList.add("area-lista-tabela-dados");
  
    adicionarDadosTabelaAluno(aluno, novoLi);
    adicionarBotoesTabelaAluno(novoLi, indice, aluno);
  
    ul.appendChild(novoLi);
  }
  
  function adicionarDadosTabelaAluno(aluno, novoLi) {
    var pMatricula = document.createElement("p");
    pMatricula.textContent = aluno.matricula;
    novoLi.appendChild(pMatricula);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  
    var pNome = document.createElement("p");
    pNome.textContent = aluno.nome;
    novoLi.appendChild(pNome);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  
    var pProfessor = document.createElement("p");
    pProfessor.textContent = aluno.professor;
    novoLi.appendChild(pProfessor);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  
    var pSala = document.createElement("p");
    pSala.textContent = aluno.sala;
    novoLi.appendChild(pSala);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  
    var pSerie = document.createElement("p");
    pSerie.textContent = aluno.serie;
    novoLi.appendChild(pSerie);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  
    var pTurno = document.createElement("p");
    horarioEstudo = aluno.turno === "1" ? "Manhã" : "Tarde";
    pTurno.textContent = horarioEstudo;
    novoLi.appendChild(pTurno);
  
    var span1 = document.createElement("span");
    novoLi.appendChild(span1);
  }

  function adicionarBotoesTabelaAluno(novoLi, indice, aluno) {
    var divBotoes = document.createElement("div");
  
    var botaoEditar = document.createElement("button");
    botaoEditar.id = "alunos-bt-editar" + indice;
    botaoEditar.classList.add("area-lista-dados-bt-editar");
    botaoEditar.textContent = "Editar";
    divBotoes.appendChild(botaoEditar);
  
    var botaoApagar = document.createElement("button");
    botaoApagar.id = "alunos-bt-deletar" + indice;
    botaoApagar.classList.add("area-lista-dados-bt-apagar");
    botaoApagar.textContent = "Apagar";
    divBotoes.appendChild(botaoApagar);
  
    novoLi.appendChild(divBotoes);
  
    botaoEditar.addEventListener("click", function () {
      abrirTelaEdicaoAluno(aluno);
    });
  
    botaoApagar.addEventListener("click", function () {
      abrirTelaDeletarAluno(indice, aluno);
    });
  }