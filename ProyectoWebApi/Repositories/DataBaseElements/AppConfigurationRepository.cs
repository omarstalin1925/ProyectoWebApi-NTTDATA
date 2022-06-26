using  ProyectoWebApi.BaseService;
using ProyectoWebApi.BaseService;
//using ProyectoWebApi.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ProyectoWebApi.BaseService.Repositories.DataBaseElements
{
    public class AppConfigurationRepository : IAppConfigurationRepository
    {
        private readonly ApplicationDbContext _configurationContext;

        public AppConfigurationRepository(ApplicationDbContext configurationContext)
        {
            _configurationContext = configurationContext;
        }

        public async  Task<bool> SaveAsync()
        {
            try
            {
                return (await _configurationContext.SaveChangesAsync() >= 0);
            }
            catch (Exception ex)
            {
                //_configurationContext.Reset();
                throw ex;
            }
        }
    }
}
