# Clientes

Esse projeto é uma implementação de cadastro de clientes seguindo princípios de Domain-Driven Design (DDD) e Command Query Responsibility Segregation (CQRS).

## Execução simplificada

1 - Baixe e instale o Docker em sua máquina;
2 - Execute o arquivo "up.bat".

## Tecnologias Utilizadas

- .NET Core 8: Framework utilizado para o desenvolvimento da aplicação.
- Entity Framework Core: Para interagir com o banco de dados relacional (SQL Server).
- SQL Server: Banco de dados relacional para escrita e local para facilitar a execução.
- Auto History: Utilizado biblioteca de Auto History para gravar histórico de dados ao alterar e incluir
- MongoDB: Banco de dados NoSQL utilizado para leitura.
- MediatR: Para mensageria interna e implementação do padrão CQRS.
- Swagger: Para documentação e testes da API.
- FluentValidation: Para validação de comandos.
- xUnit e Moq: Para testes unitários no backend.
- React: Frontend.
- Typescript: Frontend desenvolvido em typescript.
- Containers: Aplicação possui containers para facilitar a execução.
- UnForm: Para formulários, foi tipado de modo a aproveitar o recurso como um todo.
- MaterialUi: Para estilização do frontend.
- YUP: Para validações.
- EsLint: para padronização do projeto.
- PWA: O Sistema pode ser instalado utilizando a técnologia de PWA.
- Tema: Possui temas claro e escuro.

## Arquitetura BackEnd

- Domain: Contém as entidades de domínio, eventos e interfaces de repositórios.
- Infra: Contém a implementação dos repositórios e contextos de dados.
- Application: Contém comandos, consultas, manipuladores.
- API: Camada de apresentação, contém os controladores.

## Arquitetura FrontEnd

- pages: onde ficam as paginas.
- routes: onde ficam as rotas.
- shared: onde ficam componentes compartilhados -> components, contexts, environments, forms, hooks, layouts, services, themes.

### Funcionalidades

- Cadastro de Clientes: Inclui operações de criação, leitura, atualização e exclusão (CRUD).
- CQRS Implementado: Separação clara entre operações de escrita e leitura.
- Mensageria Interna: Utilização do MediatR para comunicação entre componentes da aplicação para sincronizar dados entre o banco de escrita e leitura.
- Validações com FluentValidation: Validações de comandos de entrada.
- Validações com YUP: validações de formulário no frontend.

## Estrutura do Projeto Backend

```sh
Clientes/backend/Clientes
│
├── src/
│   ├── Clientes.API/
│   │   ├── Controllers/
│   │   ├── Program.cs
│   │   └── ...
│   ├── Clientes.Application/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Events/
│   │   └── ...
│   ├── Clientes.Domain/
│   │   ├── ClientAggregate/
│   │   ├── Data/
│   │   ├── DomainObjects/
│   │   └── ...
│   ├── Clientes.Infra/
│   │   ├── Contexts/
│   │   ├── Repositories/
│   │   └── ...
|   tests/
│   ├── Clientes.Tests/
│   |   ├── Commands/
│   |   ├── Queries/
│   |   ├── Events/
│   |   └── ...
├── README.md
└── Dockerfile
```

## Estrutura do Projeto Fronted

```sh
Clientes/frontend/clientes
│
├── public/
│   ├── index.html
│   ├── manifest.json
│   └── ...
├── src/
│   ├── pages/
│   │   ├── clientes/
│   │   ├── dashboard/
│   │   └── ...
│   ├── routes/
│   │   └── index.tsx
│   ├── shared/
│   │   ├── componets/
│   │   ├── contexts/
│   │   ├── environments/
│   │   └── ...
|   tests/
│   ├── Clientes.Tests/
│   |   ├── Commands/
│   |   ├── Queries/
│   |   ├── Events/
│   |   └── ...
├── README.md
└── Dockerfile
```

#### Testes Unitários Backend

Os testes unitários para comandos, consultas e manipuladores de eventos são implementados utilizando xUnit e Moq para mock. Estes testes garantem a integridade das operações de criação, leitura, atualização e exclusão (CRUD) e a correta publicação de eventos.

##### Executando os Testes Backend

Para executar os testes unitários, utilize o seguinte comando no diretório do projeto de testes:

```sh
dotnet test
```

## Estrutura da Solução
Todo o código do backend e frontend está contido em duas soluções e repositórios, organizada em pastas backend e frontend para facilitar a navegação e a execução do projeto.


# Para Criar uma Migration
É necessário estar no diretório que contenha o Contexto do EF Core da aplicação.

`cd ./src/Clientes.Infra`

Em seguida execute o comando para gerar a nova migration apontando para o projeto de startup, esse projeto deve ter obrigatoriamente o pacote Microsoft.EntityFrameworkCore.Design instalado.

Mesmo sendo uma má pratica utilizá-lo, haja visto que é uma necessidade inerente a utilização da estratégia do Database Tenant Provider ©. :).

`dotnet ef migrations add --startup-project ../Clientes.Api [Nome da sua migration]`

Se atente para o nome da sua migration, ela será imutável e irá perdurar por toda a vida do projeto :p, o EF Core irá criar um prefixo com a data atual, portanto não é necessário inserir no nome da migration nenhuma informação referente a data que você está criando-a.

# Para Atualizar a Base de Dados
Ainda no diretório que se encontra o Context da aplicação, execute o comando:

`dotnet ef database update --startup-project ../Clientes.Api`

* Atenção, em ambiente de desenvolvimento esse script irá executar as migrations para a base relacionada ao tenant de referencia, localizado no appSettings.Development.json sob a chave ReferenceTenantId.

# Para Visualizar o SQL da Migration
Ainda no diretório que se encontra o Context da aplicação, execute o comando:

`dotnet ef migrations script --startup-project ../Clientes.Api`
