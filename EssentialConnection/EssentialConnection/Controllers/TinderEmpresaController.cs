using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace EssentialConnection.Controllers
{
    public class TinderEmpresaController : Controller
    {
        public readonly Context _context;
        public TinderEmpresaController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int pagina = 1)
        {
            var context = _context.Curriculo.Include(i => i.ItensCurriculo).Include(c => c.Compentencias);
            return View((await context.ToListAsync()).ToPagedList(pagina, 1));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int curriculoId, string nomeAluno)
        {
            TinderEmpresa tinderEmpresa = new TinderEmpresa();
            tinderEmpresa.CurriculoId = curriculoId;
            tinderEmpresa.nomeAluno = nomeAluno;
            //tinderEmpresa.EmpresaId = User.Identity.GetUserId();
            _context.Add(tinderEmpresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ListarTodos()
        {
            var context = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa);
            ViewData["CurriculoId"] = new SelectList(_context.Curriculo.OrderBy(c => c.CurriculoID), "CursoID", "Nome");
            return View(await context.ToListAsync());
        }
        //[HttpPost]
        //public async Task<IActionResult> ListarTodos(string competencias)
        //{
        //    ViewData["CurriculoId"] = new SelectList(_context.Curriculo.OrderBy(c => c.CurriculoID), "CursoID", "Nome");
        //    var listar = _context.Curriculo.Where(s =>s.Compentencias.==competencias);
        //    return View(await listar.ToListAsync());
        //}
    }
}
