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
    public class CompentenciasController : Controller
    {
        private readonly Context _context;

        public CompentenciasController(Context context)
        {
            _context = context;
        }

        // GET: Compentencias
        public async Task<IActionResult> Index()
        {
            var context = _context.Compentencia.Include(c => c.Curriculo);
            return View(await context.ToListAsync());
        }

        // GET: Compentencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compentencias = await _context.Compentencia
                .Include(c => c.Curriculo)
                .FirstOrDefaultAsync(m => m.CompentenciasID == id);
            if (compentencias == null)
            {
                return NotFound();
            }

            return View(compentencias);
        }

        // GET: Compentencias/Create
        public IActionResult Create()
        {
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID");
            return View();
        }

        // POST: Compentencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompentenciasID,Descricao,CurriculoId")] Compentencias compentencias)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(compentencias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID", compentencias.CurriculoId);
            return View(compentencias);
        }

        // GET: Compentencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compentencias = await _context.Compentencia.FindAsync(id);
            if (compentencias == null)
            {
                return NotFound();
            }
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID", compentencias.CurriculoId);
            return View(compentencias);
        }

        // POST: Compentencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompentenciasID,Descricao,CurriculoId")] Compentencias compentencias)
        {
            if (id != compentencias.CompentenciasID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(compentencias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompentenciasExists(compentencias.CompentenciasID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo, "CurriculoID", "CurriculoID", compentencias.CurriculoId);
            return View(compentencias);
        }

        // GET: Compentencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compentencias = await _context.Compentencia
                .Include(c => c.Curriculo)
                .FirstOrDefaultAsync(m => m.CompentenciasID == id);
            if (compentencias == null)
            {
                return NotFound();
            }

            return View(compentencias);
        }

        // POST: Compentencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compentencias = await _context.Compentencia.FindAsync(id);
            _context.Compentencia.Remove(compentencias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompentenciasExists(int id)
        {
            return _context.Compentencia.Any(e => e.CompentenciasID == id);
        }
    }
}
