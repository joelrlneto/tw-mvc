using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.Paciente;

namespace WebApplication4.Controllers
{
    public class PacientesController : Controller
    {
        private readonly SisMedContext _context;

        public PacientesController(SisMedContext context)
        {
            _context = context;
        }

        // GET: PacientesController
        public ActionResult Index(string filtro)
        {
            var pacientes = new List<ListarPacienteViewModel>();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                pacientes = _context.Pacientes.Where(p => p.Nome.Contains(filtro) || p.CPF.Contains(filtro.Replace(".", "").Replace("-", "")))
                                              .Select(p => new ListarPacienteViewModel
                                              {
                                                  Id = p.Id,
                                                  Nome = p.Nome,
                                                  CPF = p.CPF
                                              })
                                              .ToList();
            }

            return View(pacientes);
        }

        // GET: PacientesController/Create
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: PacientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AdicionarPacienteViewModel dados)
        {
            try
            {
                var paciente = new Paciente
                {
                    Nome = dados.Nome,
                    CPF = dados.CPF.Replace(".","").Replace("-",""),
                    DataNascimento = dados.DataNascimento
                };

                _context.Pacientes.Add(paciente);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacientesController/Details/5
        public ActionResult Editar(int id)
        {
            var paciente = _context.Pacientes.Find(id);
                                             
            if (paciente != null)
            {
                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                return View(new EditarPacienteViewModel
                {
                    Id = paciente.Id,
                    CPF = paciente.CPF,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento,
                    Alergias = informacoesComplementares?.Alergias,
                    MedicamentosEmUso = informacoesComplementares?.MedicamentosEmUso,
                    CirurgiasRealizadas = informacoesComplementares?.CirurgiasRealizadas
                });
            }
            else
            {
                return NotFound();
            }
        }

        // POST: PacientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarPacienteViewModel dados)
        {
            try
            {
                var paciente = _context.Pacientes.Find(id);
                paciente.CPF = dados.CPF.Replace(".", "").Replace("-", "");
                paciente.Nome = dados.Nome;
                paciente.DataNascimento = dados.DataNascimento;

                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                if (informacoesComplementares == null)
                    informacoesComplementares = new InformacoesComplementaresPaciente();

                informacoesComplementares.Alergias = dados.Alergias;
                informacoesComplementares.MedicamentosEmUso = dados.MedicamentosEmUso;
                informacoesComplementares.CirurgiasRealizadas = dados.CirurgiasRealizadas;
                informacoesComplementares.IdPaciente = id;

                if (informacoesComplementares.Id > 0)
                    _context.InformacoesComplementaresPaciente.Update(informacoesComplementares);
                else
                    _context.InformacoesComplementaresPaciente.Add(informacoesComplementares);

                _context.Pacientes.Update(paciente);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacientesController/Excluir/5
        public ActionResult Excluir(int id)
        {
            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                return View(new EditarPacienteViewModel
                {
                    Id = paciente.Id,
                    CPF = paciente.CPF,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento
                });
            }
            else
            {
                return NotFound();
            }
        }

        // POST: PacientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id, IFormCollection collection)
        {
            try
            {
                var paciente = _context.Pacientes.Find(id);
                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                _context.InformacoesComplementaresPaciente.Remove(informacoesComplementares);
                _context.Pacientes.Remove(paciente);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Buscar(string filtro)
        {
            var pacientes = new List<ListarPacienteViewModel>();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                pacientes = _context.Pacientes.Where(p => p.Nome.Contains(filtro) || p.CPF.Contains(filtro.Replace(".", "").Replace("-", "")))
                                              .Take(10)
                                              .Select(p => new ListarPacienteViewModel
                                              {
                                                  Id = p.Id,
                                                  Nome = p.Nome,
                                                  CPF = p.CPF
                                              })
                                              .ToList();
            }

            return Json(pacientes);
        }
    }
}
