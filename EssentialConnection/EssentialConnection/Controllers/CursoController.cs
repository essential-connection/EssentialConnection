using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EssentialConnection.Controllers
{
    public class CursoController : Controller
    { 
        private readonly Context _context;

        public CursoController(Context contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Curso.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Curso curso)
        {
            _context.Add(curso);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var curso = _context.Curso.Find(id);
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Curso curso)
        {
            _context.Entry(curso).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var curso = _context.Curso
                .FirstOrDefault(p => p.CursoID == id);
            return View(curso);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Curso curso)
        {
            _context.Remove(curso);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var curso = _context.Curso
                .FirstOrDefault(c => c.CursoID == id);
            return View(curso);
        }


    }
}
