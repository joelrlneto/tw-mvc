namespace WebApplication4.Models.Entities
{
    public class Medico
    {
        public Medico(string crm, string nome)
        {
            CRM = crm;
            Nome = nome;
        }

        public int Id { get; set; }
        public string CRM { get; set; }
        public string Nome { get; set; }
        public ICollection<Consulta>? Consultas { get; set; }
    }
}
