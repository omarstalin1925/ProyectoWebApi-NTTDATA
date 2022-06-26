using ProyectoWebApi.Entities;

namespace  ProyectoWebApi.Repositories
{
    public interface ICuentaRepository
    {
        Task<Cuenta> InsertarCuenta(Cuenta cuenta);
        Task<Cuenta> EditarCuenta(Cuenta cuenta);
        Task<Cuenta> BuscarPorId(int id);
        Task<List<Cuenta>> BuscarPorIdCliente(int id);
        Task<Cuenta> EliminarCuenta(Cuenta cuenta);
        Task<Cuenta> ObtenerCuenta(string numCuenta);
    }
}