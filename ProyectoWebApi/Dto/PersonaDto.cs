using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Dto
{
    public partial class PersonaDto
    {
        public PersonaDto()
        {
            Cliente = new HashSet<ClienteDto>();
        }

        public int PersonaId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<ClienteDto> Cliente { get; set; }
    }
}
