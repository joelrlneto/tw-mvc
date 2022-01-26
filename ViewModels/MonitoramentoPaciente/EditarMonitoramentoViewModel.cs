using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.MonitoramentoPaciente
{
    public class EditarMonitoramentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Pressão arterial")]
        public string PressaoArterial { get; set; } = String.Empty;


        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Temperatura")]
        public decimal Temperatura { get; set; }


        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Saturação de oxigênio")]
        public int SaturacaoOxigenio { get; set; }


        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Frequência cardíaca")]
        public int FrequenciaCardiaca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data de aferição")]
        public DateTime DataAfericao { get; set; }
    }
}
