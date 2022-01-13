namespace WebApplication4.Models.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public string CRM { get; set; }
        public string Nome { get; set; }
        public ICollection<Consulta> Consultas { get; set; }
    }
}
