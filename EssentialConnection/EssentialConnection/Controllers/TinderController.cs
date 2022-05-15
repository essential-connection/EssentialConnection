using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EssentialConnection.Controllers
{
    [Authorize]
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
            
            return View();
        }
    }
}
