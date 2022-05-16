using System.ComponentModel.DataAnnotations;

namespace EssentialConnection.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        [Required]
        public string? NomeAluno { get; set; }
        [Required]
        public string? Mensagem { get; set; }
        [Required]
        public DateTime Data { get; set; }

        public int UsuarioID { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
