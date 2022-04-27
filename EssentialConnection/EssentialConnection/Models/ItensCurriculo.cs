namespace EssentialConnection.Models
{
    public class ItensCurriculo
    {
        public int ItensCurriculoID { get; set; }
        public enum Tipo { };
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Instituicao { get; set; }
        public Curriculo Curriculo { get; set; }

    }
}
