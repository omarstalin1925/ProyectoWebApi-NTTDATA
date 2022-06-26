using System;
using System.Collections.Generic;

namespace  ProyectoWebApi.Entities
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimiento = new HashSet<Movimiento>();
        }

        public int CuentaId { get; set; }
        public int? ClienteId { get; set; }
        public string? NumCuenta { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<Movimiento> Movimiento { get; set; }

    }
}
