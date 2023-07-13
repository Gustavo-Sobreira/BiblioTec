function handleNetworkError(error) {

  const p_erro = document.getElementById("pg-erros");
  const campo_erro = document.getElementById("pg-erros-campo-de-erros");

  p_erro.classList.remove("hidden");
  campo_erro.innerHTML = "[ ERRO ] - Verifique se os campos estão devidamente preenchidos e se por um acaso o programa foi encerrado, abra-o novamente para que possamos continuar.", error;
}

async function cadastrarLivro() {
  var registroRecebido = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Registro']"
  ).value;
  var tituloRecebido = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Titulo']"
  ).value;
  var autorRecebido = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Autor']"
  ).value;
  var generoRecebido = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Genero']"
  ).value;
  var editoraRecebida = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Editora']"
  ).value;
  var prateleiraRecebida = document.querySelector(
    "#pg-livros-cadastrar-formulario input[name='Prateleira']"
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
    const response = await fetch(URL_LIVRO + "/cadastro", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: jsonlivro,
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
      campo_sucesso.innerHTML = `Livro ${data.value.titulo}, foi cadastrado com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}
