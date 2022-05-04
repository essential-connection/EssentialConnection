namespace EssentialConnection.Models
{
    public class Curriculo
    {
        public int CurriculoID { get; set; }
        public string DescricaoPessoal { get; set; }
        public int? AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public ICollection<Compentencias> Compentencias { get; set; }

        public ICollection<ItensCurriculo> ItensCurriculo { get; set; }

    }
}
