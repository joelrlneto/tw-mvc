﻿using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.Medico;

namespace WebApplication4.Controllers
{
    public class MedicosController : Controller
    {
        private readonly SisMedContext _context;

        public MedicosController(SisMedContext context)
        {
            _context = context;
        }

        // GET: MedicosController
        public ActionResult Index(string filtro)
        {
            var medicos = new List<ListarMedicoViewModel>();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                medicos = _context.Medicos.Where(m => m.Nome.Contains(filtro) || m.CRM.Contains(filtro.Replace("/", "").Replace("-", "")))
                                          .Select(p => new ListarMedicoViewModel
                                          {
                                               Id = p.Id,
                                               Nome = p.Nome,
                                               CRM = p.CRM
                                          })
                                          .ToList();
            }

            return View(medicos);
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
            try
            {
                var medico = new Medico
                {
                    Nome = dados.Nome,
                    CRM = dados.CRM.Replace("/", "").Replace("-", "")
                };

                _context.Medicos.Add(medico);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
            else
            {
                return NotFound();
            }
        }

        // POST: MedicosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarMedicoViewModel dados)
        {
            try
            {
                var medico = _context.Medicos.Find(id);
                medico.CRM = dados.CRM.Replace("/", "").Replace("-", "");
                medico.Nome = dados.Nome;
                _context.Medicos.Update(medico);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MedicosController/Delete/5
        public ActionResult Excluir(int id)
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
            else
            {
                return NotFound();
            }
        }

        // POST: MedicosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id, EditarMedicoViewModel dados)
        {
            try
            {
                var medico = _context.Medicos.Find(id);

                _context.Medicos.Remove(medico);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
