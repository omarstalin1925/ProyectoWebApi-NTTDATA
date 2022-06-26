
using ProyectoWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using ProyectoWebApi.BaseService;

namespace ProyectoWebApi.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly ApplicationDbContext _docConfigurationContext;

        public MovimientoRepository(ApplicationDbContext docConfigurationContext)
        {
            _docConfigurationContext = docConfigurationContext;
        }
        public async Task<Movimiento> BuscarPorId(int id)
        {
            return (await _docConfigurationContext.Movimiento
               .FirstOrDefaultAsync(f => f.MovimientoId.Equals(id)));
        }

        public async Task<List<Movimiento>> BuscarPorIdCuenta(int id)
        {
            return (await _docConfigurationContext.Movimiento.Where
               (f => f.CuentaId.Equals(id)).ToListAsync());
        }

        public async Task<Movimiento> InsertarMovimiento(Movimiento movimiento)
        {
            var mov = await _docConfigurationContext.AddAsync(movimiento);
            _docConfigurationContext.SaveChanges();
            return movimiento;
        }

        public async Task<Movimiento> EditarMovimiento(Movimiento movimiento)
        {
            _docConfigurationContext.Update(movimiento);
            _docConfigurationContext.SaveChanges();
            return movimiento;
        }

        public async Task<Movimiento> ObtenerMovimiento(int id)
        {
            var mov = await _docConfigurationContext.Movimiento.Where(p => p.MovimientoId == id).FirstOrDefaultAsync();
            return mov;
        }

        public async Task<Movimiento> EliminarMovimiento(Movimiento movimiento)
        {
            var mov = _docConfigurationContext.Remove(movimiento);
            _docConfigurationContext.SaveChanges();
            return movimiento;
        }

        public async Task<List<Movimiento>> ObtenerMovimientoDebitoFechaHoy(int id)
        {
            var fechaHoy = DateTime.Now.ToString("dd-MM-yyyy");
            var mov = await _docConfigurationContext.Movimiento.
                Where(p => p.CuentaId == id
                && p.TipoMovimiento == "Debito"
                && p.Fecha == Convert.ToDateTime(fechaHoy)).
                ToListAsync();
            return mov;
        }
    }
}
