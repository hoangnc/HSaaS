using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MasterData.Companies;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class EfCoreCompanyRepository : EfCoreRepository<IMasterDataDbContext, Company, long>,
        ICompanyRepository
    {
        public EfCoreCompanyRepository(IDbContextProvider<IMasterDataDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Company> GetByIdAsync(long id)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
