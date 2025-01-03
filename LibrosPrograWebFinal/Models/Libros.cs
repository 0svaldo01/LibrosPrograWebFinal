using System;
using System.Collections.Generic;

namespace LibrosPrograWebFinal.Models;

public partial class Libros
{
    public int IdLibro { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? AñoPublicacion { get; set; }

    public int? IdGenero { get; set; }

    public int? IdAutor { get; set; }

    public virtual Autores? IdAutorNavigation { get; set; }

    public virtual Generosliterarios? IdGeneroNavigation { get; set; }
}
