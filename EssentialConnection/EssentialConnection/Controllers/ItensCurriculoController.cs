using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EssentialConnection.Controllers
{
    public class ItensCurriculoController : Controller
    {
        private readonly Context _context;

        public ItensCurriculoController(Context context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ItensCurriculo.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.ItensCurriculo
                .FirstOrDefaultAsync(m => m.ItensCurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItensCurriculoID,Tipo,Nome,Descricao,DataInicio,DataFim,Instituicao,CurriculoId")] ItensCurriculo curriculos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curriculos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curriculos);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.ItensCurriculo.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }
            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItensCurriculoID,Tipo,Nome,Descricao,DataInicio,DataFim,Instituicao,CurriculoId")] ItensCurriculo curriculos)
        {
            if (id != curriculos.ItensCurriculoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalExists(curriculos.ItensCurriculoID))
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
            return View(curriculos);
        }

        // GET: Personals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculos = await _context.ItensCurriculo
                .FirstOrDefaultAsync(m => m.ItensCurriculoID == id);
            if (curriculos == null)
            {
                return NotFound();
            }

            return View(curriculos);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personal = await _context.ItensCurriculo.FindAsync(id);
            _context.ItensCurriculo.Remove(personal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalExists(int id)
        {
            return _context.ItensCurriculo.Any(e => e.ItensCurriculoID == id);
        }
    }
}
