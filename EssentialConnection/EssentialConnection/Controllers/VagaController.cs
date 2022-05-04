using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EssentialConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EssentialConnection.Controllers
{
    public class VagaController :Controller
    {
        public Context Context;

        public VagaController(Context ctx)
        {
            Context = ctx;
        }

        public IActionResult index() 
        {
            return View(Context.Vaga
            .Include(e => e.Empresa)
            .Include(x => x.Curso));
        }

        public IActionResult Create()
        {
           // ViewBag.EmpresaID = new SelectList(Context.Empresa.OrderBy(e => e.Nome),
           //   "EmpresaID", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vaga vaga)
        {
            Context.Add(vaga);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var vaga = Context.Vaga
                .Include(e => e.Empresa)
                .Include(x => x.Curso)
                .FirstOrDefault(v => v.VagaID == id);
            return View(vaga);
        }

        public IActionResult Edit(int id)
        {
            var vaga = Context.Vaga.Find(id);
            ViewBag.EmpresaID = new SelectList(Context.Empresa.OrderBy(e => e.Nome), "EmpresaID", "Nome");
            return View(vaga);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vaga vaga)

        {
            Context.Entry(vaga).State = EntityState.Modified;
            Context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var vaga = Context.Vaga
                .Include(e => e.Empresa)
                .Include(x => x.Curso)
                .FirstOrDefault(v => v.VagaID == id);
            return View(vaga);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Vaga vaga)
        {
            Context.Remove(vaga);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
