using LibrosPrograWebFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibrosPrograWebFinal.Controllers
{
	public class HomeController : Controller
	{
		private readonly LibreriaprograwebContext context;

		public HomeController(LibreriaprograwebContext context)
        {
			this.context = context;
		}
        public IActionResult Index()
		{
			var datos = context.Libros.OrderBy(x => x.Titulo).Include(x => x.IdGeneroNavigation).Include(x=>x.IdAutorNavigation);
			return View(datos);
		}
		[Route("DetallesLibro/{titulo}")]
		public IActionResult DetallesLibro(string titulo)
		{
			var datos = context.Libros.Include(x => x.IdGeneroNavigation).Include(x => x.IdAutorNavigation).FirstOrDefault(x => x.Titulo == titulo.Replace("-", " "));
			if (datos == null) 
			{
				return RedirectToAction("Index");
			}
			return View(datos);
		}
	}
}
