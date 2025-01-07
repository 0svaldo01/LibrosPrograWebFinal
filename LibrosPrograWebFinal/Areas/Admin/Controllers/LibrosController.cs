using LibrosPrograWebFinal.Areas.Admin.Models;
using LibrosPrograWebFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LibrosPrograWebFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LibrosController : Controller
    {
		private readonly LibreriaprograwebContext context;

		public LibrosController(LibreriaprograwebContext context)
        {
			this.context = context;
		}
        #region C.R.U.D

        #region Read
        public IActionResult Index()
        {
            IndexLibrosViewModel vm = new();
            vm.Libros = context.Libros.OrderBy(x => x.Titulo).Include(x=>x.IdAutorNavigation).Include(x=>x.IdGeneroNavigation) ;
            vm.Autores = context.Autores.OrderBy(x => x.NombreAutor);
            vm.Generosliterarios = context.Generosliterarios.OrderBy(x => x.NombreGenero);

            return View(vm);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Agregar()
        {
            AgregarLibroViewModel lib = new AgregarLibroViewModel();

            lib.Autores = context.Autores.OrderBy(x => x.NombreAutor);
            lib.Generosliterarios = context.Generosliterarios.OrderBy(x => x.NombreGenero);
            

            return View(lib);
        }
        [HttpPost]
        public IActionResult Agregar(AgregarLibroViewModel l, IFormFile archivo1)
        {
            var existe = context.Libros.Any(x => x.IdLibro == l.Libro.IdLibro);

			if (archivo1.ContentType != "image/jpeg")
			{
				ModelState.AddModelError("", "Solo están permitidas imagenes .jpg");
				return View(l);
			}
			if (archivo1.Length > 1024 * 1024 * 5)
			{
				ModelState.AddModelError("", "No se permiten imagenes mayores a 5MB");
				return View(l);
			}

			if (existe)
            {
                ModelState.AddModelError("", "El libro ya esta registrado");
            }
            if (string.IsNullOrEmpty(l.Libro.Titulo))
            {
                ModelState.AddModelError("", "El título no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(l.Libro.Descripcion))
            {
                ModelState.AddModelError("", "La descripción no puede estar vacía.");
            }
            if (l.Libro.AñoPublicacion == null)
            {
                ModelState.AddModelError("", "El año de publicacion no puede estar vacio.");
            }
            if (l.Libro.AñoPublicacion < 1000 || l.Libro.AñoPublicacion > 9999)
            {
                ModelState.AddModelError("", "Ingrese un año valido.");
            }
            if (l.Libro.IdGenero == 0)
            {
                ModelState.AddModelError("", "Ingresa un genero");
            }
            if (l.Libro.IdAutor == 0) 
            {
                ModelState.AddModelError("", "Ingresa un autor");
            }
            if (ModelState.IsValid)
            {
                context.Add(l.Libro);
                context.SaveChanges();

				var path = "wwwroot/images/" + l.Libro.IdLibro + ".jpg";
				FileStream fs = new FileStream(path, FileMode.Create);
				archivo1.CopyTo(fs);
				fs.Close();

                return Redirect("~/Admin/Libros/Index");
			}

            return View(l);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Editar(int id)
        {
            AgregarLibroViewModel lib = new AgregarLibroViewModel();

            lib.Libro = context.Libros.FirstOrDefault(x => x.IdLibro == id);

            lib.Autores = context.Autores.OrderBy(x => x.NombreAutor);
            lib.Generosliterarios = context.Generosliterarios.OrderBy(x => x.NombreGenero);

            if(lib.Libro == null)
            {
                return Redirect("~/Admin/Libros");
            }

            return View(lib);
        }
        [HttpPost]
        public IActionResult Editar(AgregarLibroViewModel l)
        {
            var existe = context.Libros.FirstOrDefault(x => x.IdLibro == l.Libro.IdLibro && x.IdLibro != l.Libro.IdLibro);

            if (existe != null)
            {
                ModelState.AddModelError("", "El libro ya esta registrado");
            }
            if (string.IsNullOrEmpty(l.Libro.Titulo))
            {
                ModelState.AddModelError("", "El título no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(l.Libro.Descripcion))
            {
                ModelState.AddModelError("", "La descripción no puede estar vacía.");
            }
            if (l.Libro.AñoPublicacion == null)
            {
                ModelState.AddModelError("", "El año de publicacion no puede estar vacio.");
            }
            if (l.Libro.AñoPublicacion < 1000 || l.Libro.AñoPublicacion > 9999)
            {
                ModelState.AddModelError("", "Ingrese un año valido.");
            }
            if (l.Libro.IdGenero == 0)
            {
                ModelState.AddModelError("", "Ingresa un genero");
            }
            if (l.Libro.IdAutor == 0)
            {
                ModelState.AddModelError("", "Ingresa un autor");
            }

            var libroedt = context.Libros.FirstOrDefault(x=>x.IdLibro == l.Libro.IdLibro);

            if(libroedt == null)
            {
                return Redirect("~/Admin/Libros");
            }


            if (ModelState.IsValid && libroedt !=null)
            {
                libroedt.Titulo = l.Libro.Titulo;
                libroedt.Descripcion = l.Libro.Descripcion;
                libroedt.AñoPublicacion = l.Libro.AñoPublicacion;
                libroedt.IdAutor = l.Libro.IdAutor;
                libroedt.IdGenero = l.Libro.IdGenero;


                context.Update(libroedt);
                context.SaveChanges();
                return Redirect("~/Admin/Libros/Index");
            }

            return View(l);
        }
        #endregion

        #region Delate
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var libroen = context.Libros.FirstOrDefault(x => x.IdLibro == id);
            if (libroen == null)
            {
                return Redirect("~/Admin/Libros");
            }

            return View(libroen);
        }

        [HttpPost]
        public IActionResult Eliminar(Libros p)
        {
            var libroelim = context.Libros.FirstOrDefault(x => x.IdLibro == p.IdLibro);

            if (libroelim == null)
            {

				return Redirect("~/Admin/Libros");
			}
			else
            {
                context.Remove(libroelim);
                context.SaveChanges();

                return Redirect("~/Admin/Libros");
            }

            return View(p);
        }

        #endregion

        #endregion
    }
}
