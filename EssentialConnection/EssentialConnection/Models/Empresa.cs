namespace EssentialConnection.Models
{
    public class Empresa
    {
        public int EmpresaID { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public ICollection<Vaga> Vagas { get; set; }
    }
}
