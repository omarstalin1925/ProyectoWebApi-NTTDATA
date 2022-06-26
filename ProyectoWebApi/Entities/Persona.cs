using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Entities
{
    public partial class Persona
    {
        public Persona()
        {
            Cliente = new HashSet<Cliente>();
        }

        public int PersonaId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
    }
}
