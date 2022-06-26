using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Dto
{
    public partial class ClienteDto
    {
        public ClienteDto()
        {
            Cuenta = new HashSet<CuentaDto>();
        }

        public int ClienteId { get; set; }
        public int PersonaId { get; set; }
        public string Contrasenia { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual UsuarioDto Persona { get; set; } = null!;
        public virtual ICollection<CuentaDto> Cuenta { get; set; }
    }
}
