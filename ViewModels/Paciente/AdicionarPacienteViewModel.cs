using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.Paciente
{
    public class AdicionarPacienteViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CPF { get; set; } = String.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; } = String.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
