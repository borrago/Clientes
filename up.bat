@echo off

REM Salva o diret�rio atual
set "diretorio_atual=%CD%"

REM Construir e iniciar os cont�ineres em segundo plano
docker compose up -d --build

REM Espera alguns segundos para garantir que os cont�ineres estejam totalmente iniciados
timeout /t 10

REM Abre o navegador na URL especificada
start "Clientes" http://localhost:3000/home
