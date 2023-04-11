using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Context
{
    public class TarefaDbContext : DbContext
    {
        public TarefaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
