using System;
using System.Collections.Generic;

namespace LibrosPrograWebFinal.Models;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
