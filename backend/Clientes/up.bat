@echo off

REM Salva o diretório atual
set "diretorio_atual=%CD%"

REM Construir e iniciar os contêineres em segundo plano
docker compose up -d --build

REM Espera alguns segundos para garantir que os contêineres estejam totalmente iniciados
timeout /t 10

REM Abre o navegador na URL especificada
start "Clientes" http://localhost/swagger/index.html
