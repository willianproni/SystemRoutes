using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystem.Data;
using Model.MongoDb;
using Services;
using Microsoft.AspNetCore.Http;

namespace MVCMongoDbRouteSystem.Controllers
{
    public class RoutesController : Controller
    {
        private readonly MVCMongoDbRouteSystemContext _context;

        public RoutesController(MVCMongoDbRouteSystemContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index(FormFile excel)
        {
            return View( ReadFileExcel.ReadXls(excel).OrderBy(rotas => rotas.Cep));
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            return View();
        }

/*        public IActionResult ReadXls()
        {
            var rotas = ReadFileExcel.ReadXls();
            return RedirectToAction(nameof(Index), rotas);
        }*/

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OS,DataRota,StatusRota,Auditado,CopRevertue,LogRota,Pdf,Foto,Contrato,Assinante,Tecnicos,LoginUser,Cop,UltimoAlterar,LocalAtuacao,Ponto,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOS,Endereco,Numero,Complemento,Bairro,Cep,NodeRota,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,OS,DataRota,StatusRota,Auditado,CopRevertue,LogRota,Pdf,Foto,Contrato,Assinante,Tecnicos,LoginUser,Cop,UltimoAlterar,LocalAtuacao,Ponto,Cidade,Base,Horario,Segmento,Servico,TipoServico,TipoOS,Endereco,Numero,Complemento,Bairro,Cep,NodeRota,Pacote,Cod,Telefone1,Telefone2,Obs,ObsTecnico,Equipamento")] Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Route
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var route = await _context.Route.FindAsync(id);
            _context.Route.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(string id)
        {
            return _context.Route.Any(e => e.Id == id);
        }
    }
}
