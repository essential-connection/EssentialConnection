namespace EssentialConnection.Models
{
    public class Vaga
    {
        public int? VagaID { get; set; } 
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Status { get; set; }
        public string Responsavel { get; set; }

        public Empresa Empresa { get; set; }

        public Curso Curso { get; set; }

        public ICollection<Aluno>? Alunos { get; set; }
    }
}
