
using ProyectoWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using  ProyectoWebApi.BaseService;

namespace  ProyectoWebApi.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext _docConfigurationContext;
        public PersonaRepository(ApplicationDbContext docConfigurationContext)
        {
            _docConfigurationContext = docConfigurationContext;
        }
        public async Task<Persona> BuscarPorId(int id)
        {
            return (await _docConfigurationContext.Persona
               .FirstOrDefaultAsync(f => f.PersonaId.Equals(id)));
        }
        public async Task<Persona> BuscarPorIdentificacion(string identificacion)
        {
            return (await _docConfigurationContext.Persona
                .FirstOrDefaultAsync(f => f.Identificacion.Equals(identificacion)));
        }
        public async Task<Persona> EditarPersona(Persona persona)
        {
            var per = _docConfigurationContext.Update(persona);
            _docConfigurationContext.SaveChanges();
            return persona;
        }
        public async Task<Persona> EliminarPersona(Persona persona)
        {
            var per = _docConfigurationContext.Remove(persona);
            _docConfigurationContext.SaveChanges();
            return persona;
        }

        public async Task<Persona> InsertarPersona(Persona persona)
        {
            var per = await _docConfigurationContext.AddAsync(persona);
            _docConfigurationContext.SaveChanges();
            return persona;
        }

        public async Task<Persona> ObtenerPersona(string identificacion)
        {
            var per = await _docConfigurationContext.Persona.Include(p=>p.Cliente).Where(b=>b.Identificacion==identificacion).FirstOrDefaultAsync();
            return per;
        }

    }
}
