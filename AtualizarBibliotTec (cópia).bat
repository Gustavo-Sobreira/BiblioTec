@echo off

REM Configurações
set "repo_owner=Gustavo-Sobreira"  REM Substitua pelo nome de usuário do GitHub
set "repo_name=BiblioTec"  REM Substitua pelo nome do repositório
set "branch_name=main"  REM Substitua pelo nome da branch

REM Caminho de destino do repositório
set "destination=C:\Users\Sobreira\Desktop\BiblioTec"  REM Substitua pelo caminho do diretório do repositório existente

REM Função para atualizar o repositório do GitHub
:update_repository
REM Navega para o diretório do repositório
cd /d "%destination%"

REM Executa o comando "git pull" para atualizar o repositório
git pull origin %branch_name%

REM Verifica se a atualização foi bem-sucedida
if %errorlevel% equ 0 (
    echo O repositório foi atualizado com sucesso.
) else (
    echo Falha ao atualizar o repositório.
)

