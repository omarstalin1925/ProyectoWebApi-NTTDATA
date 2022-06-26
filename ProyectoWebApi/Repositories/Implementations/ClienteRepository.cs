
using ProyectoWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using  ProyectoWebApi.BaseService;

namespace  ProyectoWebApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _docConfigurationContext;

        public ClienteRepository(ApplicationDbContext docConfigurationContext)
        {
            _docConfigurationContext = docConfigurationContext;
        }
        public async Task<Cliente> BuscarPorIdPersona(int id)
        {
            return (await _docConfigurationContext.Cliente
               .FirstOrDefaultAsync(f => f.PersonaId.Equals(id)));
        }
        public async Task<Cliente> BuscarPorId(int id)
        {
            return (await _docConfigurationContext.Cliente
               .FirstOrDefaultAsync(f => f.ClienteId.Equals(id)));
        }
        public async Task<Cliente> EditarCliente(Cliente cliente)
        {
            _docConfigurationContext.Update(cliente);
            _docConfigurationContext.SaveChanges();
            return cliente;
        }
        public async Task<Cliente> EliminarCliente(Cliente cliente)
        {
            var cli = _docConfigurationContext.Remove(cliente);
            _docConfigurationContext.SaveChanges();
            return cliente;
        }
        public async Task<Cliente> InsertarCliente(Cliente cliente)
        {
            var per = await _docConfigurationContext.AddAsync(cliente);
            _docConfigurationContext.SaveChanges();
            return cliente;
        }
    }
}
