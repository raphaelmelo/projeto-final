# Projeto To Do List

Api para aplicação `To Do List`

### O que foi utilizado no Backend?
 - Linguagem: C#
 - Asp.Net Core Web API versão 6.0
 - Conceito de inversão de controle com injeção de dependência do repositório da model e do contexto
 - ORM Entity Framework para acesso aos dados e persistência dos dados em um banco de dados
 - PostGreSQL via `Docker` e online através do site `render.com`
 - Entity Framework Core versão 6
    - CRUD básico utilizando o Base Repository com Insert, Get, Put (Update), e Delete
 - Documentação via Swagger

### Para executar a api localmente

- Possuir o Docker instalado
- Executar `docker-compose -f docker-compose.yml up -d` na pasta backend
- Após executar o comando, para subir o banco de dados via docker, alterar a connection string para "Docker" no arquivo Program.cs, linha 40.


      builder.Services.AddDbContext<TarefaDbContext>(options =>
      {
          options.UseNpgsql(builder.Configuration.GetConnectionString("Docker"),
              m => m.MigrationsAssembly("TodoList"));
      });
