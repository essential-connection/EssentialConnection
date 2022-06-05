using Microsoft.AspNetCore.Mvc;

namespace EssentialConnection.Controllers
{
    public class PaginaInicialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
