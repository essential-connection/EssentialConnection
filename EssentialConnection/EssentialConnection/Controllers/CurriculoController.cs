#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EssentialConnection.Models;

namespace EssentialConnection.Controllers
{
    public class CurriculoController : Controller
    {
        private readonly Context _context;

        public CurriculoController(Context context)
        {
            _context = context;
        }

        // GET: Curriculo
        public async Task<IActionResult> Index()
        {
            var context = _context.Curriculo.Include(c => c.Aluno);
            return View(await context.ToListAsync());
        }

        // GET: Curriculo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(m => m.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // GET: Curriculo/Create
        public IActionResult Create()
        {
            ViewData["CurriculoID"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID");
            return View();
        }

        // POST: Curriculo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoID,DescricaoPessoal,AlunoId")] Curriculo curriculo)
        {
            _context.Add(curriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["CurriculoID"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID", curriculo.CurriculoID);
            return View(curriculo);
        }

        // GET: Curriculo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }
            ViewData["CurriculoID"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID", curriculo.CurriculoID);
            return View(curriculo);
        }

        // POST: Curriculo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoID,DescricaoPessoal,AlunoId")] Curriculo curriculo)
        {
            if (id != curriculo.CurriculoID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(curriculo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurriculoExists(curriculo.CurriculoID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["CurriculoID"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID", curriculo.CurriculoID);
            return View(curriculo);
        }

        // GET: Curriculo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(m => m.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // POST: Curriculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curriculo = await _context.Curriculo.FindAsync(id);
            _context.Curriculo.Remove(curriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculo.Any(e => e.CurriculoID == id);
        }
    }
}
