using LibrosPrograWebFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrosPrograWebFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Administrador")]
	[Authorize]

    public class GenerosController : Controller
    {
        private readonly LibreriaprograwebContext context;

        public GenerosController(LibreriaprograwebContext context)
        {
            this.context = context;
        }
        #region C.R.U.D

        #region Read
        public IActionResult Index()
        {
            var data = context.Generosliterarios.OrderBy(x => x.NombreGenero);
            return View(data);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Agregar()
        {
            var gen = new Generosliterarios();
            return View(gen);
        }
        [HttpPost]
        public IActionResult Agregar(Generosliterarios g)
        {
            var existe = context.Generosliterarios.Any(x => x.NombreGenero == g.NombreGenero);

            if (existe)
            {
                ModelState.AddModelError("", "El genero ya esta registrado");
            }
            if (string.IsNullOrEmpty(g.NombreGenero))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el genero");
            }

            if (ModelState.IsValid)
            {
                context.Add(g);
                context.SaveChanges();
                return Redirect("~/Admin/Generos/Index");
            }

            return View(g);
        }
        #endregion
        #region Update
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var generoencontrado = context.Generosliterarios.FirstOrDefault(x => x.IdGenero == id);
            if (generoencontrado == null)
            {
                return Redirect("~/Admin/Generos/Index");
            }
            return View(generoencontrado);
        }
        [HttpPost]
        public IActionResult Editar(Generosliterarios g)
        {
            var existe = context.Generosliterarios.Any(x => x.NombreGenero == g.NombreGenero && x.IdGenero != g.IdGenero);

            if (existe)
            {
                ModelState.AddModelError("", "El genero literario ya esta registrado");
            }
            if (string.IsNullOrEmpty(g.NombreGenero))
            {
                ModelState.AddModelError("", "Debes escribir un nombre para el genero literario");
            }


            var genedt = context.Generosliterarios.FirstOrDefault(x => x.IdGenero == g.IdGenero);

            if (ModelState.IsValid)
            {
                genedt.NombreGenero = g.NombreGenero ;        

                context.SaveChanges();
                return Redirect("~/Admin/Generos/Index");
            }

            return View(g);
        }
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var gen = context.Generosliterarios.FirstOrDefault(c => c.IdGenero == id);
            if (gen == null)
            {
                return Redirect("~/Admin/Generos/Index");
            }

            return View(gen);
        }

        [HttpPost]
        public IActionResult Eliminar(Generosliterarios g)
        {
            var genelm = context.Generosliterarios.FirstOrDefault(x => x.IdGenero == g.IdGenero);
            var librosgen = context.Libros.Any(x => x.IdGenero == g.IdGenero);

            if (librosgen)
            {
                ModelState.AddModelError("", "No se puede eliminar un genero con libros asociados al mismo");
            }

            if (ModelState.IsValid && genelm != null && librosgen == false)
            {
                context.Remove(genelm);
                context.SaveChanges();
                return Redirect("~/Admin/Generos/Index");
            }
            return View(genelm);
        }
    }
    #endregion
    #endregion
}

