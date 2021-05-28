using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterData.Modules;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class EfCoreModuleRepository : EfCoreRepository<IMasterDataDbContext, Module, long>,
        IModuleRepository
    {
        public EfCoreModuleRepository(IDbContextProvider<IMasterDataDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Module> GetByIdAsync(long id)
        {
            return await (await GetDbSetAsync())
               .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Module> GetByCodeAsync(string code)
        {
            return await (await GetDbSetAsync())
              .FirstOrDefaultAsync(c => c.Code == code);
        }
    }
}
