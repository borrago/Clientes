set "diretorio_atual=%CD%"

docker compose up -d --build

start "Clientes" http://localhost:80/