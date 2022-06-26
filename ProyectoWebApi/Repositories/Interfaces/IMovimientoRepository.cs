using ProyectoWebApi.Entities;

namespace  ProyectoWebApi.Repositories
{
    public interface IMovimientoRepository
    {
        Task<Movimiento> InsertarMovimiento(Movimiento movimiento);
        Task<Movimiento> EditarMovimiento(Movimiento movimiento);
        Task<Movimiento> BuscarPorId(int id);
        Task<List<Movimiento>> BuscarPorIdCuenta(int id);
        Task<Movimiento> EliminarMovimiento(Movimiento movimiento);
        Task<Movimiento> ObtenerMovimiento(int id);
        Task<List<Movimiento>> ObtenerMovimientoDebitoFechaHoy(int CuentaId);
    }
}