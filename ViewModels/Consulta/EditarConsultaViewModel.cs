﻿using System.ComponentModel.DataAnnotations;
using WebApplication4.Models.Enums;

namespace WebApplication4.ViewModels.Consulta
{
    public class EditarConsultaViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public TipoConsulta Tipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Paciente")]
        public int IdPaciente { get; set; }
        
        public string NomePaciente { get; set; } = String.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Médico")]
        public int IdMedico { get; set; }
    }
}
