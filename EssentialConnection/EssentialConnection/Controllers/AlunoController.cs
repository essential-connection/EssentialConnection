using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EssentialConnection.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EssentialConnection.Controllers
{
    public class AlunoController : Controller
    {
        private readonly Context _context;
        AlunoController(Context context)
        {
            _context = context;
        }
        // GET: AlunoController
        public ActionResult Index()
        {
            return View();
        }


        // GET: AlunoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = _context.Aluno.FirstOrDefault(m => m.AlunoID == id);
            return View(aluno);
        }


        // GET: AlunoController/Create
        public ActionResult Create(Aluno aluno)
        {
            ViewBag.AlunoID = new SelectList(_context.Aluno.OrderBy(a => a.Nome), "AlunoID", "Nome");
            return View();
        }

        // POST: AlunoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return RedirectToAction("Index");    
        }

        // GET: AlunoController/Edit/5
        public ActionResult Edit(int id)
        {
            var aluno = _context.Aluno.Find(id);
            ViewBag.AlunoID = new SelectList(_context.Aluno.OrderBy(a => a.Nome), "AlunoID", "Nome");
            return View();
        }

        // POST: AlunoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection,Aluno aluno)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _context.Entry(aluno).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
        }

        // GET: AlunoController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.AlunoID = new SelectList(_context.Aluno.OrderBy(a => a.Nome), "AlunoID", "Nome");
            return View();
        }

        // POST: AlunoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection, Aluno aluno)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _context.Remove(aluno);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
