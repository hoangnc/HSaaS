using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Appendixes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DocumentManagement.EntityFrameworkCore
{
    public class EfCoreAppendixRepository : EfCoreRepository<IDocumentManagementDbContext, Appendix, long>,
        IAppendixRepository
    {
        public EfCoreAppendixRepository(IDbContextProvider<IDocumentManagementDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Appendix> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(d => d.Name == name);
        }
    }
}
