using EssentialConnection.Areas.Identity.Data;
using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNet.Identity;

namespace EssentialConnection.Controllers
{
    public class TinderEmpresaController : Controller
    {
        public readonly IdentityContext _identityContext;
        public readonly Context _context;
        public TinderEmpresaController(Context context, IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }
        //public async Task<IActionResult> Index(int pagina = 1)
        //{
            
        //    //var context = _context.Curriculo.Include(i => i.ItensCurriculo).Include(c => c.Compentencias).Include(a=>a.Aluno);
        //    //return View((await context.ToListAsync()).ToPagedList(pagina, 1));
        //}

        [HttpPost]
        public async Task<IActionResult> Index(string nomeCompleto, int curriculoID)
        {
            TinderEmpresa tinderEmpresa = new TinderEmpresa();
            var userLogado = _identityContext.Users.FirstOrDefault(x => x.Id == User.Identity.GetUserId());
            if (userLogado.Tipo==EssentialConnectionUser.TipoUsuario.Professor)
            {
                var userlog = _context.Curso.FirstAsync(x => x.Email == userLogado.Email);
                tinderEmpresa.CursoNome = userlog.Result.Nome;
                tinderEmpresa.CursoId = userlog.Result.CursoID;
                tinderEmpresa.EmpresaId = null;
                tinderEmpresa.NomeEmpresa = null;
            }
            else
            {
                var userlog = _context.Empresa.FirstAsync(x => x.Email == userLogado.Email);
                tinderEmpresa.NomeEmpresa = userlog.Result.Nome;
                tinderEmpresa.EmpresaId = userlog.Result.EmpresaID;
                tinderEmpresa.CursoId = null;
                tinderEmpresa.CursoNome = null;
            }
            tinderEmpresa.CurriculoId = curriculoID;
            tinderEmpresa.NomeAluno = nomeCompleto;
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
