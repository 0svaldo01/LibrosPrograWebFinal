using LibrosPrograWebFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrosPrograWebFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AutoresController : Controller
    {
        private readonly LibreriaprograwebContext context;

        public AutoresController(LibreriaprograwebContext context)
        {
            this.context = context;
        }
        #region C.R.U.D

        #region Read
        public IActionResult Index()
        {
            var data = context.Autores.OrderBy(x => x.NombreAutor);
            return View(data);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Agregar()
        {
            var aut = new Autores();
            return View(aut);
        }
        [HttpPost]
        public IActionResult Agregar(Autores a)
        {
            var existe = context.Autores.Any(x => x.NombreAutor == a.NombreAutor);

            if (existe)
            {
                ModelState.AddModelError("", "El autor ya esta registrado");
            }
            if (string.IsNullOrEmpty(a.NombreAutor))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el autor");
            }
            if (string.IsNullOrEmpty(a.Nacionalidad))
            {
                ModelState.AddModelError("", "Debes escribir la nacionalidad para el autor");
            }
            if (ModelState.IsValid)
            {
                context.Add(a);
                context.SaveChanges();
                return Redirect("~/Admin/Autores/Index");
            }

            return View(a);
        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var autorencontrado = context.Autores.FirstOrDefault(x => x.IdAutor == id);
            if (autorencontrado == null)
            {
                return Redirect("~/Admin/Autores/Index");
            }
            return View(autorencontrado);
        }
        [HttpPost]
        public IActionResult Editar(Autores a)
        {
            var existe = context.Autores.Any(x => x.NombreAutor == a.NombreAutor && x.IdAutor != a.IdAutor);

            if (existe)
            {
                ModelState.AddModelError("", "El autor ya esta registrado");
            }
            if (string.IsNullOrEmpty(a.NombreAutor))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el autor");
            }
            if (string.IsNullOrEmpty(a.Nacionalidad))
            {
                ModelState.AddModelError("", "Debes escribir la nacionalidad para el autor");
            }

            var autedt = context.Autores.FirstOrDefault(x => x.IdAutor == a.IdAutor);

            if (ModelState.IsValid)
            {
                autedt.NombreAutor = a.NombreAutor;
                autedt.FechaNacimiento = a.FechaNacimiento;
                autedt.Nacionalidad = a.Nacionalidad;

                context.SaveChanges();
                return Redirect("~/Admin/Autores/Index");
            }

            return View(a);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var aut = context.Autores.FirstOrDefault(c => c.IdAutor == id);
            if (aut == null)
            {
                return Redirect("~/Admin/Autores/Index");
            }

            return View(aut);
        }

        [HttpPost]
        public IActionResult Eliminar(Autores a)
        {
            var autelim = context.Autores.FirstOrDefault(x => x.IdAutor == a.IdAutor);
            var autlibros = context.Libros.Any(x => x.IdAutor == a.IdAutor);

            if (autlibros)
            {
                ModelState.AddModelError("", "No se puede eliminar un autor con libros regitrados a su nombre");
            }

            if (ModelState.IsValid && autelim != null && autlibros == false)
            {
                context.Remove(autelim);
                context.SaveChanges();
                return Redirect("~/Admin/Autores/Index");
            }
            
            return View(autelim);
        }
    }
    #endregion
    #endregion

}


