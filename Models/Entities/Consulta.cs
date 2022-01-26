﻿using WebApplication4.Models.Enums;

namespace WebApplication4.Models.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TipoConsulta Tipo { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public Paciente? Paciente { get; set; }
        public Medico? Medico { get; set; }
    }
}
