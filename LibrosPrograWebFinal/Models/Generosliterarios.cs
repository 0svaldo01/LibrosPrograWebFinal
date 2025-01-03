using System;
using System.Collections.Generic;

namespace LibrosPrograWebFinal.Models;

public partial class Generosliterarios
{
    public int IdGenero { get; set; }

    public string NombreGenero { get; set; } = null!;

    public virtual ICollection<Libros> Libros { get; set; } = new List<Libros>();
}
