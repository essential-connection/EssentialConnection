#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EssentialConnection.Models;
using Microsoft.AspNet.Identity;

namespace EssentialConnection.Controllers
{
    public class ItensCurriculoController : Controller
    {
        private readonly Context _context;

        public ItensCurriculoController(Context context)
        {
            _context = context;
        }

        // GET: ItensCurriculo
        public async Task<IActionResult> Index()
        {
            var context = _context.ItensCurriculo.Include(i => i.Curriculo);
            return View(await context.ToListAsync());
        }

        // GET: ItensCurriculo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itensCurriculo = await _context.ItensCurriculo
                .Include(i => i.Curriculo)
                .FirstOrDefaultAsync(m => m.ItensCurriculoID == id);
            if (itensCurriculo == null)
            {
                return NotFound();
            }

            return View(itensCurriculo);
        }

        // GET: ItensCurriculo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItensCurriculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItensCurriculoID,Nome,Descricao,DataInicio,DataFim,Instituicao")] ItensCurriculo itensCurriculo)
        {
            var alunoLogado = _context.Aluno.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
            itensCurriculo.CurriculoId = alunoLogado.CurriculoId;
            _context.Add(itensCurriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "ItensCurriculo");
        }

        // GET: ItensCurriculo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itensCurriculo = await _context.ItensCurriculo.FindAsync(id);
            if (itensCurriculo == null)
            {
                return NotFound();
            }
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID", itensCurriculo.CurriculoId);
            return View(itensCurriculo);
        }

        // POST: ItensCurriculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItensCurriculoID,Nome,Descricao,DataInicio,DataFim,Instituicao,CurriculoId")] ItensCurriculo itensCurriculo)
        {
            if (id != itensCurriculo.ItensCurriculoID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(itensCurriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItensCurriculoExists(itensCurriculo.ItensCurriculoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID", itensCurriculo.CurriculoId);
            return View(itensCurriculo);
        }

        // GET: ItensCurriculo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itensCurriculo = await _context.ItensCurriculo
                .Include(i => i.Curriculo)
                .FirstOrDefaultAsync(m => m.ItensCurriculoID == id);
            if (itensCurriculo == null)
            {
                return NotFound();
            }

            return View(itensCurriculo);
        }

        // POST: ItensCurriculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itensCurriculo = await _context.ItensCurriculo.FindAsync(id);
            _context.ItensCurriculo.Remove(itensCurriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItensCurriculoExists(int id)
        {
            return _context.ItensCurriculo.Any(e => e.ItensCurriculoID == id);
        }
    }
}
