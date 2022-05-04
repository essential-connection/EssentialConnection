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
    public class VagasController : Controller
    {
        private readonly Context _context;

        public VagasController(Context context)
        {
            _context = context;
        }

        // GET: Vagas
        public async Task<IActionResult> Index()
        {
            var context = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa);
            return View(await context.ToListAsync());
        }

        // GET: Vagas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .Include(v => v.Curso)
                .Include(v => v.Empresa)
                .FirstOrDefaultAsync(m => m.VagaID == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // GET: Vagas/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoID", "CursoID");
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaID", "EmpresaID");
            return View();
        }

        // POST: Vagas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VagaID,Nome,Descricao,Quantidade,DataExpiracao,Status,Responsavel,EmpresaId,CursoId")] Vaga vaga)
        {
                _context.Add(vaga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoID", "CursoID", vaga.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaID", "EmpresaID", vaga.EmpresaId);
            return View(vaga);
        }

        // GET: Vagas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoID", "CursoID", vaga.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaID", "EmpresaID", vaga.EmpresaId);
            return View(vaga);
        }

        // POST: Vagas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VagaID,Nome,Descricao,Quantidade,DataExpiracao,Status,Responsavel,EmpresaId,CursoId")] Vaga vaga)
        {
            if (id != vaga.VagaID)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(vaga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagaExists(vaga.VagaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["CursoId"] = new SelectList(_context.Curso, "CursoID", "CursoID", vaga.CursoId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "EmpresaID", "EmpresaID", vaga.EmpresaId);
            return View(vaga);
        }

        // GET: Vagas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .Include(v => v.Curso)
                .Include(v => v.Empresa)
                .FirstOrDefaultAsync(m => m.VagaID == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // POST: Vagas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaga = await _context.Vaga.FindAsync(id);
            _context.Vaga.Remove(vaga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VagaExists(int id)
        {
            return _context.Vaga.Any(e => e.VagaID == id);
        }
    }
}
