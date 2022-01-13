﻿namespace WebApplication4.Models.Entities
{
    public class MonitoramentoPaciente
    {
        public int Id { get; set; }
        public string PressaoArterial { get; set; }
        public decimal Temperatura { get; set; }
        public int SaturacaoOxigenio { get; set; }
        public int FrequenciaCardiaca { get; set; }
        public DateTime DataAfericao { get; set; }
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
    }
}
