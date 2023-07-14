function abrirTelaDeletarAluno(indice, aluno) {
  var pgDeletar = document.getElementById("pg-deletar");
  var campoAntigo = document.getElementById("pg-deletar-area-dados-antigos");

  var btnDeletar = document.getElementsByClassName(
    "pg-deletar-area-botoes-confirmar"
  );

  for (var i = 0; i < btnDeletar.length; i++) {
    btnDeletar[i].removeEventListener("click", pgDeletarAreaBotoesConfirmar);
    btnDeletar[i].addEventListener("click", pgDeletarAreaBotoesConfirmarAluno);
  }

  pgDeletar.classList.remove("hidden");

  preencherCamposDoEstadoAtualDoObjetoAluno(campoAntigo, aluno);
}

async function pgDeletarAreaBotoesConfirmarAluno() {
  var campoAntigo = document.getElementById("pg-deletar-area-dados-antigos");
  id = campoAntigo.firstChild.textContent;

  try {
    const response = await fetch(URL_ALUNO + "/apagar/" + id, {
      method: `DELETE`,
      headers: {
        "Content-Type": "application/json",
      },
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
      campo_sucesso.innerHTML = `Aluno ${campoAntigo.children[2].textContent}, foi deletado com sucesso!`;
    }
  } catch (error) {
    handleNetworkError(error);
  }
}
