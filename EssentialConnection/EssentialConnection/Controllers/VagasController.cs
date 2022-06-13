#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EssentialConnection.Models;
using EssentialConnection.Areas.Identity.Data;
using Microsoft.AspNet.Identity;

namespace EssentialConnection.Controllers
{
    public class VagasController : Controller
    {
        private readonly Context _context;
        private readonly IdentityContext _identityContext;

        public VagasController(Context context, IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        // GET: Vagas
        public async Task<IActionResult> Index()
        {
            var teste = 3;
            var userLogado = _identityContext.Users.FirstOrDefault(x => x.Id == User.Identity.GetUserId());
            if(userLogado.Tipo == EssentialConnectionUser.TipoUsuario.Professor)    
            {
                var user = _context.Curso.FirstOrDefault(x=>x.UserId == User.Identity.GetUserId());
                var contexto = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa).Where(x=>x.CursoId==user.CursoID);
                return View(await contexto.ToListAsync());
            }
            else
            {
                var user = _context.Empresa.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
                var contexto = _context.Vaga.Include(v => v.Curso).Include(v => v.Empresa).Where(x => x.EmpresaId == user.EmpresaID);
                return View(await contexto.ToListAsync());
            }
            
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
            return View();
        }

        // POST: Vagas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VagaID,Nome,Descricao,Quantidade,DataExpiracao,Status,Responsavel")] Vaga vaga)
        {
            //PODIA TER FEITO ESSA LÓGICA MAIS SIMPLES, MAS PRO PROVO ENTENDER SE PRECISAR EXPLICAR
            //OBSERVAÇÃO
            var userLogado = _identityContext.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            if (userLogado.Tipo == EssentialConnectionUser.TipoUsuario.Professor)
            {
                var CursoLogado=_context.Curso.FirstOrDefault(x=>x.UserId == User.Identity.GetUserId());
                vaga.CursoId = CursoLogado.CursoID;
                vaga.EmpresaId = null;
            }
            else if (userLogado.Tipo == EssentialConnectionUser.TipoUsuario.Empresa)
            {
                var EmpresaLogada = _context.Empresa.FirstOrDefault(e => e.UserId == User.Identity.GetUserId());
                vaga.EmpresaId = EmpresaLogada.EmpresaID;
                vaga.CursoId = null;
            }
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
            ViewData["CursoId"] = new SelectList(_context.Curso.OrderBy(c => c.Nome), "CursoID", "Nome");
            ViewData["EmpresaId"] = new SelectList(_context.Empresa.OrderBy(e => e.Nome), "EmpresaID", "Nome");
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
