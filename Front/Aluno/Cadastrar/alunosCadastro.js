

function handleNetworkError(error) {

  const p_erro = document.getElementById("pg-erros");
  const campo_erro = document.getElementById("pg-erros-campo-de-erros");

  p_erro.classList.remove("hidden");
  campo_erro.innerHTML = "[ ERRO ] - Verifique se os campos estão devidamente preenchidos e se por um acaso o programa foi encerrado, abra-o novamente para que possamos continuar.", error;
}

async function cadastrarAluno() {
  var matriculaRecebida = document.querySelector(
    "#pg-alunos-cadastrar-formulario input[name='Matricula']"
  ).value;
  var nomeRecebido = document.querySelector(
    "#pg-alunos-cadastrar-formulario input[name='Nome']"
  ).value;
  var salaREcebida = document.querySelector(
    "#pg-alunos-cadastrar-formulario input[name='Sala']"
  ).value;
  var serieRecebida = document.querySelector(
    "#pg-alunos-cadastrar-formulario input[name='Série']"
  ).value;
  var professorRecebido = document.querySelector(
    "#pg-alunos-cadastrar-formulario input[name='Professor']"
  ).value;
  var turnoRecebido = document.querySelector(
    "#pg-alunos-cadastrar-formulario-turno"
  ).value;

  // Verificação de campos vazios
  if (
    matriculaRecebida === "" ||
    nomeRecebido === "" ||
    salaREcebida === "" ||
    serieRecebida === "" ||
    professorRecebido === "" ||
    turnoRecebido === ""
  ) {
    alert("Preencha todos os campos obrigatórios.");
    return;
  }

  var aluno = {
    matricula: matriculaRecebida,
    nome: nomeRecebido,
    professor: professorRecebido,
    sala: salaREcebida,
    turno: turnoRecebido,
    serie: serieRecebida,
  };

  var jsonAluno = JSON.stringify(aluno);

  try {
    const response = await fetch(URL_ALUNO + "/cadastro", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: jsonAluno,
    });



    const data = await response.json();
    console.log(data)
   
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
      campo_sucesso.innerHTML = `Aluno ${data.value.nome}, foi cadastrado com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}

