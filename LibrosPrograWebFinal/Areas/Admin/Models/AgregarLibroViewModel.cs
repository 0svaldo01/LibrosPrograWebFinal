using LibrosPrograWebFinal.Models;

namespace LibrosPrograWebFinal.Areas.Admin.Models
{
    public class AgregarLibroViewModel
    {
        public Libros Libro { get; set; }
        public IEnumerable<Autores> Autores { get; set; } = new List<Autores>();
        public IEnumerable<Generosliterarios> Generosliterarios { get; set; } = new List<Generosliterarios>();

    }
}
