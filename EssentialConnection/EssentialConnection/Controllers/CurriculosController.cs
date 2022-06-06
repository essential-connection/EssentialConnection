#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EssentialConnection.Models;
using Microsoft.AspNet.Identity;

namespace EssentialConnection.Controllers
{
    public class CurriculosController : Controller
    {
        private readonly Context _context;

        public CurriculosController(Context context)
        {
            _context = context;
        }

        // GET: Curriculos
        public async Task<IActionResult> Index()
        {
            var context = _context.Curriculo.Include(c => c.Aluno);
            return View(await context.ToListAsync());
        }

        // GET: Curriculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //ViewData["CursoId"] = new SelectList(_context.Curso.OrderBy(c => c.Nome), "CursoID", "Nome");
            //ViewData["EmpresaId"] = new SelectList(_context.Empresa.OrderBy(e => e.Nome), "EmpresaID", "Nome");
            ViewData["ItensCurriculo"] = new SelectList(_context.ItensCurriculo.OrderBy(i => i.ItensCurriculoID), "ItensCurriculoID", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo
                .Include(c => c.Aluno).Include(c => c.ItensCurriculo).Include(c => c.Compentencias).Include(u => u.Aluno.Curso)
                .FirstOrDefaultAsync(m => m.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // GET: Curriculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curriculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoID,DescricaoPessoal")] Curriculo curriculo)
        {
            var alunoLogado = _context.Aluno.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
            curriculo.AlunoId = alunoLogado.AlunoID;
            _context.Add(curriculo);
            AlunosController aluno = new AlunosController(_context);
            await _context.SaveChangesAsync();
            aluno.AdicionaCurriculo(curriculo.CurriculoID, alunoLogado.AlunoID);
            return RedirectToAction("CompletandoCurriculo", "Curriculos");
        }
        
        [HttpGet]
        public IActionResult CompletandoCurriculo()
        {
            return View();
        }

        // GET: Curriculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID", curriculo.AlunoId);
            return View(curriculo);
        }

        // POST: Curriculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoID,DescricaoPessoal,AlunoId")] Curriculo curriculo)
        {
            if (id != curriculo.CurriculoID)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculoExists(curriculo.CurriculoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["AlunoId"] = new SelectList(_context.Aluno, "AlunoID", "AlunoID", curriculo.AlunoId);
            return View(curriculo);
        }

        // GET: Curriculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculo
                .Include(c => c.Aluno)
                .FirstOrDefaultAsync(m => m.CurriculoID == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        // POST: Curriculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curriculo = await _context.Curriculo.FindAsync(id);
            _context.Curriculo.Remove(curriculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculo.Any(e => e.CurriculoID == id);
        }

        public IActionResult AdicionarCompentencia(string compentenciaNome)
        {
            CompentenciasController comp = new CompentenciasController(_context);
            var alunoLogado = _context.Aluno.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
            comp.Create(compentenciaNome,alunoLogado.AlunoID);
            return RedirectToAction("CompletandoCurriculo", "Curriculos");
        }

        public IActionResult AdicionarItensCurriculo(string nome,string descricao, string instituicao, string dataInicio, string dataFim)
        {
            ItensCurriculoController itens = new ItensCurriculoController(_context);
            var alunoLogado = _context.Aluno.FirstOrDefault(x => x.UserId == User.Identity.GetUserId());
            itens.Create(nome, descricao, instituicao, dataInicio, dataFim, alunoLogado.AlunoID);
            return RedirectToAction("CompletandoCurriculo", "Curriculos");
        }
    }
}
