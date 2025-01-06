using Microsoft.AspNetCore.Mvc;

namespace LibrosPrograWebFinal.Areas.Admin.Controllers
{
    public class LibrosControllers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
