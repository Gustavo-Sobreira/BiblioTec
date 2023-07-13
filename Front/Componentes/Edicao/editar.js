

//TODO Usar essa função para criação de elementos também
async function pgEdicaoAreaBotoesConfirmar(idFormulario, metodoRequisicao, acao) {

  var registroRecebido = document.querySelector(
    `#${idFormulario}  input[name='Registro']`
  ).value;

  var tituloRecebido = document.querySelector(
    `#${idFormulario} input[name='Titulo']`
  ).value;
  var autorRecebido = document.querySelector(
    `#${idFormulario} input[name='Autor']`
  ).value;
  var generoRecebido = document.querySelector(
    `#${idFormulario} input[name='Genero']`
  ).value;
  var editoraRecebida = document.querySelector(
    `#${idFormulario} input[name='Editora']`
  ).value;
  var prateleiraRecebida = document.querySelector(
    `#${idFormulario} input[name='Prateleira']`
  ).value;

  // Verificação de campos vazios
  if (
    registroRecebido === "" ||
    tituloRecebido === "" ||
    autorRecebido === "" ||
    generoRecebido === "" ||
    prateleiraRecebida === "" ||
    editoraRecebida === ""
  ) {
    alert("Preencha todos os campos obrigatórios.");
    return;
  }

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
      method: `${metodoRequisicao}`,
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
      campo_sucesso.innerHTML = `Livro ${data.value.titulo}, foi ${acao} com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}



