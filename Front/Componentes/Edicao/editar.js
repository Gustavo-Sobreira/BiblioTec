async function pgEdicaoAreaBotoesConfirmar() {

  var registroRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Registro']`
  ).value;

  var tituloRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Titulo']`
  ).value;
  var autorRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Autor']`
  ).value;
  var generoRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Genero']`
  ).value;
  var editoraRecebida = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Editora']`
  ).value;
  var prateleiraRecebida = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Prateleira']`
  ).value;

  
  var livro = {
    registro: registroRecebido,
    titulo: tituloRecebido,
    autor: autorRecebido,
    editora: editoraRecebida,
    genero: generoRecebido,
    prateleira: prateleiraRecebida,
  };


  var jsonlivro = JSON.stringify(livro);

  try {
    const response = await fetch(URL_LIVRO + "/editar", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: jsonlivro,
    });

    const data = await response.json();

    if (data.value.toString().startsWith("[ ERRO ]")) {
      const p_erro = document.getElementById("pg-erros");
      const campo_erro = document.getElementById("pg-erros-campo-de-erros");

      p_erro.classList.remove("hidden");
      campo_erro.innerHTML = data.value;
    } else {
      const p_sucesso = document.getElementById("pg-sucesso");
      const campo_sucesso = document.getElementById(
        "pg-sucesso-campo-de-sucesso"
      );

      p_sucesso.classList.remove("hidden");
      campo_sucesso.innerHTML = `Livro ${livro.titulo}, foi editado com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}



async function pgEdicaoAreaBotoesConfirmarAluno() {

  var matriculaRecebida = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Matricula']`
  ).value;

  var NomeRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Nome']`
  ).value;

  var professorRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Professor']`
  ).value;

  var salaRecebida = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Sala']`
  ).value;

  var turnoRecebido = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Turno']`
  ).value;
  
  var serieRecebida = document.querySelector(
    `#pg-edicao-area-dados-novos input[name='Serie']`
  ).value;

   turnoRecebido = turnoRecebido == "Tarde" ? "2" : "1";

  var aluno = {
    matricula: matriculaRecebida,
    nome: NomeRecebido,
    professor: professorRecebido,
    sala: salaRecebida,
    turno: turnoRecebido,
    serie: serieRecebida
  };

  var jsonAluno = JSON.stringify(aluno);
  
  try {
    const response = await fetch(URL_ALUNO + "/editar", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: jsonAluno,
    });

    const data = await response.json();

    if (data.value.toString().startsWith("[ ERRO ]")) {
      const p_erro = document.getElementById("pg-erros");
      const campo_erro = document.getElementById("pg-erros-campo-de-erros");

      p_erro.classList.remove("hidden");
      campo_erro.innerHTML = data.value;
    } else {
      const p_sucesso = document.getElementById("pg-sucesso");
      const campo_sucesso = document.getElementById(
        "pg-sucesso-campo-de-sucesso"
      );

      p_sucesso.classList.remove("hidden");
      campo_sucesso.innerHTML = `Aluno ${aluno.nome}, foi editador com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}