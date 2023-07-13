
function abrirTelaDeletar(indice, livro) {
    var pgDeletar = document.getElementById("pg-deletar");
    var campoAntigo = document.getElementById("pg-deletar-area-dados-antigos");
  
    pgDeletar.classList.remove("hidden");
    preencherCamposDoEstadoAtualDoObjeto(campoAntigo, livro);
  }
  
  function pgDeletarAreaBotoesCancelar() {
    var pgDeletar = document.getElementById("pg-deletar");
    pgDeletar.classList.add("hidden");
  }





//TODO Usar essa função para criação de elementos também
async function pgDeletarAreaBotoesConfirmar() {
    var campoAntigo = document.getElementById("pg-deletar-area-dados-antigos");
    id = campoAntigo.firstChild.textContent;
  

    try {
      const response = await fetch(URL_LIVRO + "/apagar/" + id, {
        method: `DELETE`,
        headers: {
          "Content-Type": "application/json",
        }
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
        campo_sucesso.innerHTML = `Livro ${campoAntigo.children[2].textContent}, foi deletado com sucesso!`;
      }
    } catch (error) {
      handleNetworkError(error);
    }
  }
  
  
  
  