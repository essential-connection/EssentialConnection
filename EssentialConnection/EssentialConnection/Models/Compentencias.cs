namespace EssentialConnection.Models
{
    public class Compentencias
    {
        public int CompentenciasID { get; set; }
        public string Descricao { get; set; }
        public int? CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }


    }
}
