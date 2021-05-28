using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MasterData.Modules
{
    public interface IModuleRepository : IBasicRepository<Module>
    {
        Task<Module> GetByIdAsync(long id);
        Task<Module> GetByCodeAsync(string code);
    }
}
