
using ProyectoWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using  ProyectoWebApi.BaseService;

namespace  ProyectoWebApi.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly ApplicationDbContext _docConfigurationContext;
        public CuentaRepository(ApplicationDbContext docConfigurationContext)
        {
            _docConfigurationContext = docConfigurationContext;
        }
        public async Task<Cuenta> EditarCuenta(Cuenta cuenta)
        {
            _docConfigurationContext.Update(cuenta);
            _docConfigurationContext.SaveChanges();
            return cuenta;
        }
        public async Task<Cuenta> EliminarCuenta(Cuenta cuenta)
        {
            var mov = _docConfigurationContext.Remove(cuenta);
            _docConfigurationContext.SaveChanges();
            return cuenta;
        }
        public async Task<Cuenta> InsertarCuenta(Cuenta cuenta)
        {
            var cuent = await _docConfigurationContext.AddAsync(cuenta);
            _docConfigurationContext.SaveChanges();
            return cuenta;
        }
        public async Task<Cuenta> BuscarPorId(int id)
        {
            return (await _docConfigurationContext.Cuenta
               .FirstOrDefaultAsync(f => f.CuentaId.Equals(id)));
        }
        public async Task<List<Cuenta>> BuscarPorIdCliente(int id)
        {
            return (await _docConfigurationContext.Cuenta.Where(f => f.ClienteId.Equals(id)).ToListAsync());
        }

        public async Task<Cuenta> ObtenerCuenta(string numCuenta)
        {
            var cta = await _docConfigurationContext.Cuenta.Where(b => b.NumCuenta == numCuenta).FirstOrDefaultAsync();
            return cta;
        }
    }
}
