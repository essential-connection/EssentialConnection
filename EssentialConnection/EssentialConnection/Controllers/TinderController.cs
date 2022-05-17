using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EssentialConnection.Controllers
{
    public class TinderController : Controller
    {
        private readonly Context _context;
        public TinderController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pagina = 1)
        {
            var context = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa);
            return View((await context.ToListAsync()).ToPagedList(pagina,1));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int vagaId,string nomeVaga)
        {
            Tinder tinder = new Tinder();
            tinder.VagaId = vagaId;
           // tinder.AlunoId = User.Identity.GetUserId();
            tinder.nomeVaga = nomeVaga;
            _context.Add(tinder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListarTodos()
        {
            var context = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa);
            ViewData["CursoId"] = new SelectList(_context.Curso.OrderBy(c => c.Nome), "CursoID", "Nome");
            ViewData["EmpresaId"] = new SelectList(_context.Empresa.OrderBy(e => e.Nome), "EmpresaID", "Nome");
            return View(await context.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> ListarTodos(string nomeVaga)
        {
            ViewData["CursoId"] = new SelectList(_context.Curso.OrderBy(c => c.Nome), "CursoID", "Nome");
            ViewData["EmpresaId"] = new SelectList(_context.Empresa.OrderBy(e => e.Nome), "EmpresaID", "Nome");
            var listar = _context.Vaga.Where(s => s.Nome == nomeVaga);
            return View(await listar.ToListAsync());
        }
    }
}
