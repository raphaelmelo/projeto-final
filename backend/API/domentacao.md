# Projeto To Do List

Api para aplicação `To Do List`

### O que foi utilizado no backend?
 - Linguagem: C#
 - Asp.Net Core Web API versão 6.0
 - Conceito de inversão de controle com injeção de dependência do repositório da model e do contexto
 - ORM Entity Framework para acesso aos dados e persistência dos dados em um banco de dados
 - PostGreSQL
 - Entity Framework Core versão 6
    - CRUD básico utilizando o Base Repository com Insert, Get, Put (Update), e Delete
 - Documentação via Swagger

### O que é necessário para executar localmente

- Possuir o docker instalado
- Executar `docker-compose -f docker-compose.yml up -d` na pasta backend
