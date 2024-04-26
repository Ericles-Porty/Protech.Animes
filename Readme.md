# Protech.Animes
Este projeto se trata de um desafio técnico proposto pela empresa Protech Solutions. O desafio consiste em criar uma API RESTful para gerenciar animes.

## Funcionalidades
- Cadastro de animes
- Listagem de animes com filtros de nome, resumo e diretor, todos com paginação
- Atualização de animes
- Exclusão de animes
- Cadastro de usuários
- Autenticação de usuários
- Cadastro de diretores
- Listagem de diretores com paginação
- Atualização de diretores
- Exclusão de diretores

## Como executar o projeto
1. Clone o repositório
2. Abra o terminal na pasta do projeto
3. Entre na pasta Protech.Animes.Infrastructure `cd Protech.Animes.Infrastructure`
4. Execute o comando `dotnet ef database update -c ProtechAnimesDbContext` para criar o banco de dados
5. Volte para a pasta raiz do projeto `cd ..`
6. Execute o comando `dotnet run --project Protech.Animes.API` para iniciar a API
7. Acesse a documentação da API em `https://localhost:5001/swagger/index.html`

## Tecnologias utilizadas
- .NET 8
- PostgreSQL
- Entity Framework Core
- Swagger
  