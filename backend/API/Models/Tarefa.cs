using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Titulo { get; set; }
        [Required]
        public string? Descricao { get; set; }
    }
}
