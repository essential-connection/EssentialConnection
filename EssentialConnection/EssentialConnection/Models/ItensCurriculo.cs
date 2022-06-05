namespace EssentialConnection.Models
{
    public class ItensCurriculo
    {
        public int ItensCurriculoID { get; set; }
        public enum Tipo {Trabalho, Estagio, Curso};
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Instituicao { get; set; }
        public int? CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }

    }
}
