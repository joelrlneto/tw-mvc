﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.MonitoramentoPaciente
{
    public class AdicionarMonitoramentoViewModel
    {
        public int IdPaciente { get; set; }

        [Display(Name = "Pressão arterial")]
        public string PressaoArterial { get; set; } = String.Empty;

        
        [Display(Name = "Temperatura")]
        public decimal Temperatura { get; set; }

        
        [Range(0, 100, ErrorMessage = "A saturação deve estar entre {0} e {1}")]
        [Display(Name = "Saturação de oxigênio")]
        public int SaturacaoOxigenio { get; set; }

        
        [Display(Name = "Frequência cardíaca")]
        public int FrequenciaCardiaca { get; set; }

        [Display(Name = "Data de aferição")]
        public DateTime DataAfericao { get; set; }
    }
}
