using LibrosPrograWebFinal.Models;

namespace LibrosPrograWebFinal.Services
{
    public class LibrosServices
    {

        public IEnumerable<Autores> ListaAutores { get; set; }
        public IEnumerable<Generosliterarios> ListaGeneros {  get; set; }
        public LibrosServices(LibreriaprograwebContext context)
        {

            ListaAutores = context.Autores.OrderBy(x => x.NombreAutor);
            ListaGeneros = context.Generosliterarios.OrderBy(x => x.NombreGenero);
        }
    }
}
