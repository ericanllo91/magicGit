using System;
using System.Collections.Generic;

namespace mtg_lib.Library.Models
{
    public partial class Usuario
    {
        public int Idusuario { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
    }
}
