@echo off

REM Configurações
set "repo_owner=Gustavo-Sobreira"  REM Substitua pelo nome de usuário do GitHub
set "repo_name=BiblioTec"  REM Substitua pelo nome do repositório
set "branch_name=main"  REM Substitua pelo nome da branch

REM Caminho de destino para o download
set "destination=C:\Users\Sobreira\Desktop\BiblioTec"  REM Substitua pelo caminho de destino desejado

REM Função para baixar o repositório do GitHub
:download_repository
REM Remove o diretório existente, se existir
if exist "%destination%" (
    rmdir /s /q "%destination%"
)

REM Faz o download do repositório do GitHub usando o comando "git clone"
git clone --depth=1 --branch %branch_name% "https://github.com/%repo_owner%/%repo_name%.git" "%destination%"

REM Verifica se o download foi bem-sucedido
if %errorlevel% equ 0 (
    echo O repositório foi baixado com sucesso.
) else (
    echo Falha ao baixar o repositório.
)
