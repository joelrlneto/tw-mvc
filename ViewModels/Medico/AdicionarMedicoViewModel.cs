using System.Text.RegularExpressions;

namespace WebApplication4.ViewModels.Medico
{
    public class AdicionarMedicoViewModel
    {
        private string crm = String.Empty;
        public string Nome { get; set; } = String.Empty;
        public string CRM { get => crm; set => crm = Regex.Replace(value, "[^0-9]", ""); }
    }
}
