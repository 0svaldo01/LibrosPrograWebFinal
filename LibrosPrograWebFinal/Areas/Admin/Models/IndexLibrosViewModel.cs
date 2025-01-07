using LibrosPrograWebFinal.Models;

namespace LibrosPrograWebFinal.Areas.Admin.Models
{
    public class IndexLibrosViewModel
    {
        public int IdLibro { get; set; }
        public IEnumerable<Libros> Libros { get; set; }
        public IEnumerable<Autores> Autores { get; set; }
        public IEnumerable<Generosliterarios> Generosliterarios { get; set; }
    }
}
