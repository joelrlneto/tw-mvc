using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.Medico;

namespace WebApplication4.Controllers
{
    public class MedicosController : Controller
    {
        private readonly SisMedContext _context;
        private const int TAMANHO_PAGINA = 10;

        public MedicosController(SisMedContext context)
        {
            _context = context;
        }

        // GET: MedicosController
        public ActionResult Index(string filtro, int pagina = 1)
        {
            ViewBag.Filtro = filtro;

            var condicao = (Medico m) => String.IsNullOrWhiteSpace(filtro) || m.Nome.ToUpper().Contains(filtro.ToUpper()) || m.CRM.Contains(filtro.Replace("/", "").Replace("-", ""));

            var medicos = _context.Medicos.Where(condicao)
                                          .Select(p => new ListarMedicoViewModel
                                          {
                                              Id = p.Id,
                                              Nome = p.Nome,
                                              CRM = p.CRM
                                          });

            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / TAMANHO_PAGINA);
            return View(medicos.Skip((pagina - 1) * TAMANHO_PAGINA)
                                 .Take(TAMANHO_PAGINA)
                                 .ToList());
        }

        // GET: MedicosController/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: MedicosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AdicionarMedicoViewModel dados)
        {
            if (!ModelState.IsValid)
            {
                return View(dados);
            }

            var medico = new Medico
            {
                Nome = dados.Nome,
                CRM = Regex.Replace(dados.CRM, "[^0-9]", "")
            };

            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: MedicosController/Details/5
        public ActionResult Editar(int id)
        {
            var medico = _context.Medicos.Find(id);

            if (medico != null)
            {
                return View(new EditarMedicoViewModel
                {
                    Id = medico.Id,
                    CRM = medico.CRM,
                    Nome = medico.Nome
                });
            }
            
            return NotFound();
        }

        // POST: MedicosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarMedicoViewModel dados)
        {
            if (!ModelState.IsValid)
            {
                return View(dados);
            }
            
            var medico = _context.Medicos.Find(id);
                
            if(medico != null)
            {
                medico.CRM = Regex.Replace(dados.CRM, "[^0-9]", "");
                medico.Nome = dados.Nome;
                _context.Medicos.Update(medico);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        // GET: MedicosController/Delete/5
        public ActionResult Excluir(int id)
        {
            var medico = _context.Medicos.Find(id);

            if (medico != null)
            {
                var temConsultas = _context.Consultas.Any(c => c.IdMedico == id);

                if (temConsultas)
                    ViewBag.TemConsultas = true;

                return View(new EditarMedicoViewModel
                {
                    Id = medico.Id,
                    CRM = medico.CRM,
                    Nome = medico.Nome
                });
            }
            
            return NotFound();
        }

        // POST: MedicosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id, EditarMedicoViewModel dados)
        {
            var medico = _context.Medicos.Find(id);

            if (medico != null)
            {
                _context.Medicos.Remove(medico);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }
    }
}
