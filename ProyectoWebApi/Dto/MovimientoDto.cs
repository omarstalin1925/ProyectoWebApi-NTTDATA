using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Dto
{
    public partial class MovimientoDto
    {
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string? TipoMovimiento { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Saldo { get; set; }

        public virtual CuentaDto Cuenta { get; set; } = null!;
    }
}
