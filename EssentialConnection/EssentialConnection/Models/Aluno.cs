namespace EssentialConnection.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public int Matricula { get; set; }

        public ICollection<Vaga> Vaga { get; set; }

        public Curriculo  Curriculo { get; set; }

        public Curso Curso { get; set; }
    }
}
