using FluentValidation;
using System.Text.RegularExpressions;
using WebApplication4.Models.Contexts;
using WebApplication4.ViewModels.Medico;

namespace WebApplication4.Validators.Medico
{
    public class AdicionarMedicoValidator:AbstractValidator<AdicionarMedicoViewModel>
    {
        private readonly SisMedContext _context;
        public AdicionarMedicoValidator(SisMedContext context)
        {
            _context = context;

            RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo obrigatório")
                               .Must(crm => Regex.Replace(crm, "[^0-9]", "").Length == 11).WithMessage("CRM inválido");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório")
                                .MaximumLength(100).WithMessage("O nome deve ter até {MaxLength} caracteres");
        }
    }
}
