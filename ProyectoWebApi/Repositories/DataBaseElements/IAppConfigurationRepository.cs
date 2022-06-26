using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ProyectoWebApi.BaseService.Repositories.DataBaseElements
{
    interface IAppConfigurationRepository
    {
        Task<bool> SaveAsync();//CONTROL DE LOS CAMBIOS EN LA BDD
    }
}
