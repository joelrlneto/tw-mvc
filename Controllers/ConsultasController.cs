using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.Consulta;

namespace WebApplication4.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly SisMedContext _context;

        public ConsultasController(SisMedContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filtro)
        {
            var consultas = new List<ListarConsultaViewModel>();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                consultas = _context.Consultas.Include(c => c.Paciente)
                                              .Include(c => c.Medico)  
                                              .Where(c => c.Paciente!.Nome.Contains(filtro) || c.Medico!.Nome.Contains(filtro))
                                              .Select(c => new ListarConsultaViewModel
                                              {
                                                  Id = c.Id,
                                                  Paciente = c.Paciente!.Nome,
                                                  Medico = c.Medico!.Nome,
                                                  Data = c.Data
                                              })
                                              .ToList();
            }

            return View(consultas);
        }

        // GET: ConsultasController/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });
            return View();
        }

        // POST: ConsultasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AdicionarConsultaViewModel dados)
        {
            try
            {
                var consulta = new Consulta
                {
                    Data = dados.Data,
                    IdMedico = dados.IdMedico,
                    IdPaciente = dados.IdPaciente,
                    Tipo = dados.Tipo
                };

                _context.Consultas.Add(consulta);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            var consulta = _context.Consultas.Include(c => c.Paciente)
                                             .Include(c => c.Medico)
                                             .FirstOrDefault(c => c.Id == id);

            if (consulta != null)
            {
                ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });

                return View(new EditarConsultaViewModel
                {
                    IdMedico = consulta.IdMedico,
                    IdPaciente = consulta.IdPaciente,
                    NomePaciente = consulta.Paciente!.Nome,
                    Data = consulta.Data,
                    Tipo = consulta.Tipo
                });
            }
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarConsultaViewModel dados)
        {
            try
            {
                var consulta = _context.Consultas.Find(id);
                if(consulta != null)
                {
                    consulta.IdPaciente = dados.IdPaciente;
                    consulta.IdMedico = dados.IdMedico;
                    consulta.Data = dados.Data;
                    consulta.Tipo = dados.Tipo;

                    _context.Consultas.Add(consulta);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else return NotFound();

            }
            catch
            {
                return View();
            }
        }
    }
}
