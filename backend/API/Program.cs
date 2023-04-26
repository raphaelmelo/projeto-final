using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TodoList.Context;
using TodoList.Interfaces;
using TodoList.Repositorios;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TodoList",
                    Version = "v1",
                    Description = "Projeto de conclusão de curso - Software Product",
                    Contact = new OpenApiContact()
                    {
                        Name = "Repositório do código completo",
                        Url = new Uri("https://github.com/raphaelmelo/projeto-final")
                    }
                });
                var xmlArquivo = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlArquivo));
            });

            builder.Services.AddDbContext<TarefaDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Render"),
                    m => m.MigrationsAssembly("TodoList"));
            });

            builder.Services.AddScoped<IRepositorio, Repositorio>();

            builder.Services.AddCors(policyBuilder =>
                policyBuilder.AddDefaultPolicy(policy =>
                policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin())
            );

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TarefaDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}