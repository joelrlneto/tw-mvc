using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.Medico
{
    public class EditarMedicoViewModel
    {
        public int Id { get; set; }
        
        [MaxLength(100, ErrorMessage = "O nome do médico deve ter até {0} caracteres.")]
        public string Nome { get; set; } = String.Empty;

        [MaxLength(100, ErrorMessage = "O CRM do médico deve ter até {0} dígitos.")]
        public string CRM { get; set; } = String.Empty;
    }
}
