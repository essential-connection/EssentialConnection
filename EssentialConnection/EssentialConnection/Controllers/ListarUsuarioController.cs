using EssentialConnection.Areas.Identity.Data;
using EssentialConnection.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EssentialConnection.Areas.Identity.Data.EssentialConnectionUser;

namespace EssentialConnection.Controllers
{
    public class ListarUsuarioController : Controller
    {
        public readonly IdentityContext _identityContext;
        public readonly Context _context;
        public ListarUsuarioController(IdentityContext identityContext, Context context)
        {
            _identityContext = identityContext;
            _context = context;
        }
        public IActionResult Index()
        {
            var usuarios = _identityContext.Users.ToList();
            return View(usuarios);
        }
        public IActionResult MostrarPerfilAluno(string id)
        {
            var userProcurado = _identityContext.Users.FirstOrDefault(x => x.Id == id);
            var aluno = _context.Aluno.Include(a => a.Curso).FirstOrDefault(a => a.email == userProcurado.Email);
            return View(aluno);
        }
        public IActionResult MostrarPerfilCurso(string id)
        {
            var userProcurado = _identityContext.Users.FirstOrDefault(x => x.Id == id);
            var curso = _context.Curso.Include(c=>c.Vagas).Include(a=>a.Alunos).FirstOrDefault(a => a.Email == userProcurado.Email);
            return View(curso);
        }

        public IActionResult MostrarPerfilEmpresa(string id)
        {
            var userProcurado = _identityContext.Users.FirstOrDefault(x => x.Id == id);
            var empresa = _context.Empresa.Include(c => c.Vagas).FirstOrDefault(a => a.Email == userProcurado.Email);
            return View(empresa);
        }

        public IActionResult MostrarConnectionsAlunos(int id)
        {
            var connections = _context.Tinders.Where(x=>x.AlunoId==id);
            return View(connections.ToList());
        }
        
        public IActionResult MostrarConnectionsEmpresasCursos(int id)
        {
            var connections = _context.TinderEmpresa.Where(x => x.EmpresaId == id || x.CursoId==id);
            return View(connections.ToList());
        }        


        public IActionResult MostrarPerfilEmpresaCurso(int? idCurso, int? idEmpresa)
        {
            if (idCurso == null)
            {
                var empresa = _context.Empresa.Include(c => c.Vagas).FirstOrDefault(a => a.EmpresaID == idEmpresa);
                return RedirectToAction("MostrarPerfilEmpresa", "ListarUsuario",idEmpresa);
            }
            else
            {
                var curso = _context.Curso.Include(c => c.Vagas).FirstOrDefault(a => a.CursoID == idCurso);
                return RedirectToAction("MostrarPerfilCurso", "ListarUsuario",idCurso);
            }
        }

        public IActionResult ListarMeuPerfil()
        {
            var userLogado = _identityContext.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            if (userLogado.Tipo == EssentialConnectionUser.TipoUsuario.Professor)
            {

                return RedirectToAction("MostrarPerfilCurso", "ListarUsuario", userLogado.Id);
            }
            else if (userLogado.Tipo == EssentialConnectionUser.TipoUsuario.Empresa)
            {
                return RedirectToAction("MostrarPerfilEmpresa", "ListarUsuario", userLogado.Id);
            }
            else
            {
                return RedirectToAction("MostrarPerfilAluno", "ListarUsuario", userLogado.Id);
            }
        }
    }
}
