using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.MonitoramentoPaciente;

namespace WebApplication4.Controllers
{
    [Route("monitoramento")]
    public class MonitoramentoPacienteController : Controller
    {
        private readonly SisMedContext _context;

        public MonitoramentoPacienteController(SisMedContext context)
        {
            _context = context;
        }

        // GET: PacientesController
        public ActionResult Index(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;

            var monitoramentos = _context.MonitoramentosPaciente.Where(m => m.IdPaciente == idPaciente)
                                         .Select(m => new ListarMonitoramentoViewModel
                                         {
                                             Id = m.Id,
                                             PressaoArterial = m.PressaoArterial,
                                             SaturacaoOxigenio = m.SaturacaoOxigenio,  
                                             FrequenciaCardiaca = m.FrequenciaCardiaca,
                                             Temperatura = m.Temperatura,
                                             DataAfericao = m.DataAfericao
                                         })
                                         .ToList();

            return View(monitoramentos);
        }

        [Route("adicionar")]
        public ActionResult Adicionar(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            return View();
        }

        // POST: PacientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar")]
        public ActionResult Adicionar(AdicionarMonitoramentoViewModel dados)
        {
            try
            {
                var monitoramento = new MonitoramentoPaciente
                {
                    IdPaciente = dados.IdPaciente,
                    Temperatura = dados.Temperatura,
                    FrequenciaCardiaca = dados.FrequenciaCardiaca,
                    SaturacaoOxigenio = dados.SaturacaoOxigenio,
                    PressaoArterial = dados.PressaoArterial,
                    DataAfericao = dados.DataAfericao
                };

                _context.MonitoramentosPaciente.Add(monitoramento);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { IdPaciente = dados.IdPaciente });
            }
            catch
            {
                return View();
            }
        }

        // GET: PacientesController/Details/5
        [Route("editar/{id}")]
        public ActionResult Editar(int id)
        {
            var monitoramento = _context.MonitoramentosPaciente.Find(id);

            if (monitoramento != null)
            {
                return View(new EditarMonitoramentoViewModel
                {
                    Id = id,
                    DataAfericao = monitoramento.DataAfericao,  
                    Temperatura = monitoramento.Temperatura,
                    PressaoArterial = monitoramento.PressaoArterial,    
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio
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
        [Route("editar/{id}")]
        public ActionResult Editar(int id, EditarMonitoramentoViewModel dados)
        {
            try
            {
                var monitoramento = _context.MonitoramentosPaciente.Find(id);

                if(monitoramento != null)
                {
                    monitoramento.Temperatura = dados.Temperatura;
                    monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
                    monitoramento.FrequenciaCardiaca = dados.FrequenciaCardiaca;
                    monitoramento.DataAfericao = dados.DataAfericao;
                    monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
                    monitoramento.PressaoArterial = dados.PressaoArterial;

                    _context.MonitoramentosPaciente.Update(monitoramento);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index), new { IdPaciente = monitoramento.IdPaciente });
                }
                else return NotFound();
            }
            catch
            {
                return View();
            }
        }

        [Route("excluir/{id}")]
        public ActionResult Excluir(int id)
        {
            var monitoramento = _context.MonitoramentosPaciente.Find(id);

            if (monitoramento!= null)
            {
                return View(new EditarMonitoramentoViewModel
                {
                    DataAfericao = monitoramento.DataAfericao,
                    Temperatura = monitoramento.Temperatura,
                    PressaoArterial = monitoramento.PressaoArterial,
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio
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
        [Route("excluir/{id}")]

        public ActionResult Excluir(int id, IFormCollection collection)
        {
            try
            {
                var monitoramento = _context.MonitoramentosPaciente.Find(id);
               
               if(monitoramento != null)
               {   
                    _context.MonitoramentosPaciente.Remove(monitoramento);

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index), new { IdPaciente = monitoramento.IdPaciente });
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
