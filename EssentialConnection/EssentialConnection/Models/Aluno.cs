namespace EssentialConnection.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public string NomeCompleto { get; set; }
        public string email { get; set; }
        public string UserId { get; set; }
        public string Telefone { get; set; }
        public int? CursoId { get; set; }
        public int? CurriculoId { get; set; }
        public ICollection<Vaga>? Vagas { get; set; }
        public Curriculo  Curriculo { get; set; }
        public Curso Curso { get; set; }
    }
}
