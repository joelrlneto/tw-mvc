using FluentValidation;
using System.Text.RegularExpressions;
using WebApplication4.Models.Contexts;
using WebApplication4.ViewModels.MonitoramentoPaciente;

namespace WebApplication4.Validators.MonitoramentoPaciente
{
    public class EditarMonitoramentoPacienteValidator:AbstractValidator<EditarMonitoramentoViewModel>
    {
        private readonly SisMedContext _context;
        public EditarMonitoramentoPacienteValidator(SisMedContext context)
        {
            _context = context;

            RuleFor(x => x.SaturacaoOxigenio).Must(spo2 => spo2 >= 0 && spo2 <= 100).WithMessage("A saturação de oxigênio deve ser um valor entre 0 e 100");

            RuleFor(x => x.Temperatura).Must(temperatura => temperatura > 0).WithMessage("A temperatura não pode ser negativa");
            
            RuleFor(x => x.FrequenciaCardiaca).Must(bpm => bpm > 0).WithMessage("A frequência cardíaca não pode ser negativa");

            RuleFor(x => x.DataAfericao).NotEmpty().WithMessage("Campo obrigatório")
                                        .Must(data => data <= DateTime.Today).WithMessage("A data de aferição não pode ser futura.");

        }
    }
}
