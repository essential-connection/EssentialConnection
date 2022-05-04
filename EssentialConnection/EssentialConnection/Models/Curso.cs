namespace EssentialConnection.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public ICollection<Vaga> Vagas { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
