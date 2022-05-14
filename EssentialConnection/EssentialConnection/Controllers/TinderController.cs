using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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
    }
}
