# Define a imagem base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia os arquivos de projeto para o diretório de trabalho
COPY src/Clientes.Domain/Clientes.Domain.csproj src/Clientes.Domain/
COPY src/Clientes.Infra/Clientes.Infra.csproj src/Clientes.Infra/
COPY src/Clientes.Application/Clientes.Application.csproj src/Clientes.Application/
COPY src/Clientes.Api/Clientes.Api.csproj src/Clientes.Api/

# Restaura as dependências dos projetos
RUN dotnet restore src/Clientes.Api/Clientes.Api.csproj

# Copia todo o código-fonte para o diretório de trabalho
COPY . ./

# Restaurar pacotes NuGet
RUN dotnet restore src/Clientes.Api/Clientes.Api.csproj

# Publicar o projeto
RUN dotnet publish src/Clientes.Api/Clientes.Api.csproj -c Release -o /app/out

# Define a imagem base para a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia os arquivos publicados para o diretório de trabalho
COPY --from=build /app/out ./

# Define a variável de ambiente para configurar a data e hora para América/São Paulo
ENV TZ=America/Sao_Paulo

# Define a variável de ambiente para configurar o idioma para pt-BR
ENV LANG=pt_BR.UTF-8

# Define o comando de inicialização da aplicação
ENTRYPOINT ["dotnet", "Clientes.Api.dll"]
