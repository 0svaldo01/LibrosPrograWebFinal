using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrosPrograWebFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Administrador")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("/admin/home/index")]
        [Route("/admin/home")]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}

