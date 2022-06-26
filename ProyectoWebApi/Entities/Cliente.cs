using System;
using System.Collections.Generic;

namespace ProyectoWebApi.Entities
{
    public partial class Cliente 
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Contrasenia { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
