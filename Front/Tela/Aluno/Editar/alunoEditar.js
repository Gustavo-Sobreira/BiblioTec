function abrirTelaEdicaoAluno(aluno) {
    var pgEdicao = document.getElementById("pg-edicao");
    var campoAntigo = document.getElementById("pg-edicao-area-dados-antigos");
    var campoNovo = document.getElementById("pg-edicao-area-dados-novos");


    var btnEditar = document.getElementsByClassName(
      "pg-edicao-area-botoes-confirmar"
    );
  
    for (var i = 0; i < btnEditar.length; i++) {
      btnEditar[i].removeEventListener("click", pgEdicaoAreaBotoesConfirmar);
      btnEditar[i].addEventListener("click", pgEdicaoAreaBotoesConfirmarAluno);
    }
  
    pgEdicao.classList.remove("hidden");
  
    preencherCamposDoEstadoAtualDoObjetoAluno(campoAntigo, aluno);
    preencherCamposDoEstadoFuturoDoObjetoAluno(campoNovo, aluno);
  }
  
  function preencherCamposDoEstadoAtualDoObjetoAluno(campoAntigo, aluno) {
   
    while (campoAntigo.firstChild) {
          campoAntigo.removeChild(campoAntigo.firstChild);
      }
      
      
      var pMatricula = document.createElement("p");
      pMatricula.textContent = aluno.matricula;
      campoAntigo.appendChild(pMatricula);
  
    var pNome = document.createElement("p");
    pNome.textContent = aluno.nome;
    campoAntigo.appendChild(pNome);
  
    var pProfessor = document.createElement("p");
    pProfessor.textContent = aluno.professor;
    campoAntigo.appendChild(pProfessor);
  
    var pSala = document.createElement("p");
    pSala.textContent = aluno.sala;
    campoAntigo.appendChild(pSala);
    
    var pSerie = document.createElement("p");
    pSerie.textContent = aluno.serie;
    campoAntigo.appendChild(pSerie);
  
    var pTurno = document.createElement("p");
    horarioEstudo = aluno.turno === "1" ? "Manhã" : "Tarde";
    pTurno.textContent = horarioEstudo;
    campoAntigo.appendChild(pTurno);
  }
  
  function preencherCamposDoEstadoFuturoDoObjetoAluno(campoNovo, aluno) {
    while (campoNovo.firstChild) {
      campoNovo.removeChild(campoNovo.firstChild);
    }
  
    var pMatricula2 = document.createElement("input");
    pMatricula2.name = "Matricula";
    pMatricula2.value = aluno.matricula;
    campoNovo.appendChild(pMatricula2);
  
    var pNome2 = document.createElement("input");
    pNome2.name = "Nome";
    pNome2.value = aluno.nome;
    campoNovo.appendChild(pNome2);
  
    var pProfessor2 = document.createElement("input");
    pProfessor2.name = "Professor";
    pProfessor2.value = aluno.professor;
    campoNovo.appendChild(pProfessor2);
  
    var pSala2 = document.createElement("input");
    pSala2.name = "Sala";
    pSala2.value = aluno.sala;
    campoNovo.appendChild(pSala2);
  
    var pSerie2 = document.createElement("input");
    pSerie2.name = "Serie";
    pSerie2.value = aluno.serie;
    campoNovo.appendChild(pSerie2);
    
    var pTurno2 = document.createElement("input");
    pTurno2.name = "Turno";
    horarioEstudo = aluno.turno === "1" ? "Manhã" : "Tarde";
    pTurno2.value = horarioEstudo;
    campoNovo.appendChild(pTurno2);
  
  }
  
