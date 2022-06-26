using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Dto
{
    public partial class UsuarioDto
    {
        public string Identificacion { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        //datos cliente
        public string Contrasenia { get; set; } = null!;
        public bool? Estado { get; set; }
    }
}
