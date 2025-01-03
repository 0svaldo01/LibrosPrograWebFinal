using System;
using System.Collections.Generic;

namespace LibrosPrograWebFinal.Models;

public partial class Autores
{
    public int IdAutor { get; set; }

    public string NombreAutor { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Nacionalidad { get; set; }

    public virtual ICollection<Libros> Libros { get; set; } = new List<Libros>();
}
