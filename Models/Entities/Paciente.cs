namespace WebApplication4.Models.Entities
{
    public class Paciente
    {
        public Paciente(string cpf, string nome, DateTime dataNascimento)
        {
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public InformacoesComplementaresPaciente? InformacoesComplementares { get; set; }
        public ICollection<MonitoramentoPaciente>? Monitoramento { get; set; }
        public ICollection<Consulta>? Consultas { get; set; }
    }
}
